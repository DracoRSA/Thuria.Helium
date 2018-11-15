using System;
using System.Threading.Tasks;

using Nancy;
using Nancy.IO;
using Akka.Actor;
using Nancy.Extensions;
using Nancy.Responses.Negotiation;

using Thuria.Helium.Core;
using Thuria.Zitidar.Core;
using Thuria.Zitidar.Akka;
using Thuria.Zitidar.Nancy;
using Thuria.Helium.Akka.Core.Messages;

namespace Thuria.Helium.Akka.Nancy
{
  /// <summary>
  /// Helium Retrieve Nancy Module
  /// </summary>
  public class HeliumRetrieveModule : NancyModule
  {
    private readonly IThuriaActorSystem _tharkActorSystem;
    private readonly IThuriaSerializer _thuriaSerializer;
    private readonly IResponseNegotiator _responseNegotiator;
    private readonly IThuriaLogger _thuriaLogger;

    /// <summary>
    /// Helium Retrieve Module constructor
    /// </summary>
    /// <param name="heliumActorSystem">Thark Actor System</param>
    /// <param name="thuriaSerializer">Thuria Serializer</param>
    /// <param name="responseNegotiator">Response Negotiator</param>
    /// <param name="thuriaLogger">Thuria Logger</param>
    public HeliumRetrieveModule(IThuriaActorSystem heliumActorSystem, IThuriaSerializer thuriaSerializer, IResponseNegotiator responseNegotiator, IThuriaLogger thuriaLogger) 
      : base("helium")
    {
      _tharkActorSystem   = heliumActorSystem ?? throw new ArgumentNullException(nameof(heliumActorSystem));
      _thuriaSerializer   = thuriaSerializer ?? throw new ArgumentNullException(nameof(thuriaSerializer));
      _responseNegotiator = responseNegotiator ?? throw new ArgumentNullException(nameof(responseNegotiator));
      _thuriaLogger       = thuriaLogger ?? throw new ArgumentNullException(nameof(thuriaLogger));

      Post("/retrieve", async (parameters, token) => await ProcessRequest());
    }

    private async Task<object> ProcessRequest()
    {
      _thuriaLogger.LogMessage(LogSeverity.Info, "Received Helium Retrieve Request");

      if (Request.Body.Length <= 0)
      {
        _thuriaLogger.LogMessage(LogSeverity.Exception, "Exception: No content received in request");
        throw new BadRequestServiceErrorException("No content received in request");
      }

      try
      {
        var jsonData     = ((RequestStream)Request.Body).AsString();
        var requestModel = _thuriaSerializer.DeserializeObject<HeliumRequestModel>(jsonData);

        if (requestModel == null || requestModel.RequestData == null)
        {
          throw new Exception("Invalid / Missing Request Data Model received");
        }

        var retrieveActor       = _tharkActorSystem.ActorSystem.ActorSelection("/user/HeliumRetrieveAction");
        var actionMessage       = new HeliumActionMessage(HeliumAction.Retrieve, requestModel.RequestData);
        var actionResultMessage = await retrieveActor.Ask<HeliumActionResultMessage>(actionMessage);

        return new HeliumResponse
          {
            ActionResult = actionResultMessage.HeliumActionResult,
          };
      }
      catch (Exception runtimeException)
      {
        _thuriaLogger.LogMessage(LogSeverity.Exception, $"Exception: {runtimeException}");
        throw new InternalServerErrorException("Error occurred processing Helium Retrieve Action Request", runtimeException);
      }
    }
  }
}

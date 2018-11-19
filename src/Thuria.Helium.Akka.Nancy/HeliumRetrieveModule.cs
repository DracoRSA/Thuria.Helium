using System;
using System.Threading.Tasks;

using Nancy;
using Akka.Actor;
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
  public class HeliumRetrieveModule : HeliumBaseModule
  {
    private readonly IThuriaActorSystem _tharkActorSystem;

    /// <summary>
    /// Helium Retrieve Module constructor
    /// </summary>
    /// <param name="heliumActorSystem">Thark Actor System</param>
    /// <param name="thuriaSerializer">Thuria Serializer</param>
    /// <param name="responseNegotiator">Response Negotiator</param>
    /// <param name="thuriaLogger">Thuria Logger</param>
    public HeliumRetrieveModule(IThuriaActorSystem heliumActorSystem, IThuriaSerializer thuriaSerializer, IResponseNegotiator responseNegotiator, IThuriaLogger thuriaLogger) 
      : base(thuriaSerializer, responseNegotiator, thuriaLogger)
    {
      _tharkActorSystem = heliumActorSystem ?? throw new ArgumentNullException(nameof(heliumActorSystem));

      Post("/retrieve", async (parameters, token) => await ProcessRequest());

      ThuriaLogger.LogMessage(LogSeverity.Info, "Helium Retrieve ready to receive requests");
    }

    private async Task<object> ProcessRequest()
    {
      ThuriaLogger.LogMessage(LogSeverity.Info, "Received Helium Retrieve Request");

      try
      {
        var (requestModel, errorResponse) = GetHeliumRequest();
        if (errorResponse != null)
        {
          return errorResponse;
        }

        var retrieveActor       = _tharkActorSystem.ActorSystem.ActorSelection("/user/HeliumRetrieveAction");
        var actionMessage       = new HeliumActionMessage(HeliumAction.Retrieve, requestModel.RequestData);
        var actionResultMessage = await retrieveActor.Ask<HeliumActionResultMessage>(actionMessage);

        var heliumResponse = new HeliumResponse
          {
            ActionResult = actionResultMessage.HeliumActionResult,
            ResultData   = actionResultMessage.ResultData,
            ErrorDetail  = actionResultMessage.ErrorDetail
          };

        ThuriaLogger.LogMessage(LogSeverity.Info, $"Completed Helium Retrieve Request [{heliumResponse.ActionResult}]");

        return CreateResponse(Context, HttpStatusCode.OK, heliumResponse);
      }
      catch (Exception runtimeException)
      {
        ThuriaLogger.LogMessage(LogSeverity.Exception, $"Exception: {runtimeException}");
        throw new InternalServerErrorException("Error occurred processing Helium Retrieve Action Request", runtimeException);
      }
    }
  }
}

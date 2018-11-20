using System;
using System.Threading.Tasks;

using Nancy;
using Akka.Actor;
using Nancy.Responses.Negotiation;

using Thuria.Helium.Core;
using Thuria.Zitidar.Akka;
using Thuria.Zitidar.Core;
using Thuria.Zitidar.Nancy;
using Thuria.Helium.Akka.Core.Messages;

namespace Thuria.Helium.Akka.Nancy
{
  /// <summary>
  /// Helium Insert Nancy Module
  /// </summary>
  public class HeliumInsertModule : HeliumBaseModule
  {
    private readonly IThuriaActorSystem _heliumActorSystem;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="heliumActorSystem">Helium Actor System</param>
    /// <param name="thuriaSerializer">Thuria Serializer</param>
    /// <param name="responseNegotiator">Nancy Response Negotiator</param>
    /// <param name="thuriaLogger">Thuria Logger</param>
    public HeliumInsertModule(IThuriaActorSystem heliumActorSystem, IThuriaSerializer thuriaSerializer, IResponseNegotiator responseNegotiator, IThuriaLogger thuriaLogger) 
      : base(thuriaSerializer, responseNegotiator, thuriaLogger)
    {
      _heliumActorSystem = heliumActorSystem ?? throw new ArgumentNullException(nameof(heliumActorSystem));

      Post("/insert", async (parameters, token) => await ProcessRequest());

      ThuriaLogger.LogMessage(LogSeverity.Info, "Helium Insert ready to receive requests");
    }

    private async Task<object> ProcessRequest()
    {
      ThuriaLogger.LogMessage(LogSeverity.Info, "Received Helium Insert Request");

      try
      {
        var (requestModel, errorResponse) = GetHeliumRequest();
        if (errorResponse != null)
        {
          return errorResponse;
        }

        var retrieveActor       = _heliumActorSystem.ActorSystem.ActorSelection("/user/HeliumInsertAction");
        var actionMessage       = new HeliumActionMessage(HeliumAction.Insert, requestModel.RequestData);
        var actionResultMessage = await retrieveActor.Ask<HeliumActionResultMessage>(actionMessage);

        var heliumResponse = new HeliumResponse
          {
            ActionResult = actionResultMessage.HeliumActionResult,
            ErrorDetail  = actionResultMessage.ErrorDetail
          };

        ThuriaLogger.LogMessage(LogSeverity.Info, $"Completed Helium Insert Request [{heliumResponse.ActionResult}]");

        return CreateResponse(Context, HttpStatusCode.OK, heliumResponse);
      }
      catch (Exception runtimeException)
      {
        ThuriaLogger.LogMessage(LogSeverity.Exception, $"Exception: {runtimeException}");
        throw new InternalServerErrorException("Error occurred processing Helium Insert Action Request", runtimeException);
      }
    }
  }
}

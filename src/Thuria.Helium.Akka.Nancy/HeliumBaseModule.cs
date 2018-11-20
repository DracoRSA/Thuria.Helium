using System;

using Nancy;
using Nancy.IO;
using Nancy.Extensions;
using Nancy.Responses.Negotiation;

using Thuria.Helium.Core;
using Thuria.Zitidar.Core;

namespace Thuria.Helium.Akka.Nancy
{
  /// <summary>
  /// Helium Nancy Base Module
  /// </summary>
  public abstract class HeliumBaseModule : NancyModule
  {
    private readonly IResponseNegotiator _responseNegotiator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="thuriaSerializer">Thuria Serializer</param>
    /// <param name="responseNegotiator">Nancy Response Negotiator</param>
    /// <param name="thuriaLogger">Thuria Logger</param>
    protected HeliumBaseModule(IThuriaSerializer thuriaSerializer, IResponseNegotiator responseNegotiator, IThuriaLogger thuriaLogger)
      : base("helium")
    {
      _responseNegotiator = responseNegotiator ?? throw new ArgumentNullException(nameof(responseNegotiator));
      ThuriaSerializer    = thuriaSerializer ?? throw new ArgumentNullException(nameof(thuriaSerializer));
      ThuriaLogger        = thuriaLogger ?? throw new ArgumentNullException(nameof(thuriaLogger));
    }

    /// <summary>
    /// Thuria Serializer
    /// </summary>
    protected IThuriaSerializer ThuriaSerializer { get; }

    /// <summary>
    /// Thuria Logger
    /// </summary>
    protected IThuriaLogger ThuriaLogger { get; }

    /// <summary>
    /// Retrieve and Validate the current Helium Request Data Model
    /// </summary>
    /// <returns>Helium Request Data Model</returns>
    protected (HeliumRequestModel, Response) GetHeliumRequest()
    {
      if (Request.Body.Length <= 0)
      {
        return (null, CreateResponse(Context, HttpStatusCode.BadRequest, "No content received in request"));
      }

      var jsonData     = ((RequestStream)Request.Body).AsString();
      var requestModel = ThuriaSerializer.DeserializeObject<HeliumRequestModel>(jsonData);

      if (requestModel == null || requestModel.RequestData == null)
      {
        return (null, CreateResponse(Context, HttpStatusCode.BadRequest, "Invalid / Missing Request Data Model received"));
      }

      return (requestModel, null);
    }

    /// <summary>
    /// Create the Nancy Response
    /// </summary>
    /// <param name="nancyContext">Nancy Context</param>
    /// <param name="statusCode">Http Response Status Code</param>
    /// <param name="outputDataModel">Output Data Model</param>
    /// <returns></returns>
    protected Response CreateResponse(NancyContext nancyContext, HttpStatusCode statusCode, object outputDataModel)
    {
      nancyContext.NegotiationContext = new NegotiationContext();
      var negotiator                  = new Negotiator(nancyContext)
                                                .WithStatusCode(statusCode)
                                                .WithModel(outputDataModel);

      return _responseNegotiator.NegotiateResponse(negotiator, nancyContext);
    }
  }
}

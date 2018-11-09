using System;
using System.Threading.Tasks;

using Nancy;
using Nancy.Responses.Negotiation;

using Thuria.Zitidar.Core;

namespace Thuria.Helium.Akka.Nancy
{
  /// <summary>
  /// Helium Retrieve Nancy Module
  /// </summary>
  public class HeliumRetrieveModule : NancyModule
  {
    private readonly IThuriaSerializer _thuriaSerializer;
    private readonly IResponseNegotiator _responseNegotiator;
    private readonly IThuriaLogger _thuriaLogger;

    public HeliumRetrieveModule(IThuriaSerializer thuriaSerializer, IResponseNegotiator responseNegotiator, IThuriaLogger thuriaLogger) 
      : base("helium")
    {
      _thuriaSerializer   = thuriaSerializer ?? throw new ArgumentNullException(nameof(thuriaSerializer));
      _responseNegotiator = responseNegotiator ?? throw new ArgumentNullException(nameof(responseNegotiator));
      _thuriaLogger       = thuriaLogger ?? throw new ArgumentNullException(nameof(thuriaLogger));

      Post("/retrieve", async (parameters, token) => await ProcessRequest());
    }

    private async Task<Response> ProcessRequest()
    {
      throw new NotImplementedException();
    }
  }
}

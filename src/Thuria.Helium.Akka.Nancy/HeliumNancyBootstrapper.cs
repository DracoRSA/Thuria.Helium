using System;

using Nancy;
using StructureMap;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;

using Thuria.Zitidar.Nancy;
using Thuria.Zitidar.Serialization;

namespace Thuria.Helium.Akka.Nancy
{
  /// <summary>
  /// Helium nancy Bootstrapper
  /// </summary>
  public class HeliumNancyBootstrapper : NancyBootstrapper
  {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="structuremapContainer">Structuremap IOC Container</param>
    /// <param name="enableTracing">Enable Tracing indicator</param>
    public HeliumNancyBootstrapper(IContainer structuremapContainer, bool enableTracing = false) 
      : base(structuremapContainer, enableTracing)
    {
    }

    /// <summary>
    /// Get the current Internal Configuration
    /// </summary>
    protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration
    {
      get
      {
        var nancyInternalConfig = NancyInternalConfiguration.WithOverrides(configuration =>
          {
            configuration.Serializers.Remove(typeof(ThuriaJsonSerializer));
            configuration.StatusCodeHandlers = new[] { typeof(ThuriaCustomErrorHandler) };
            configuration.ResponseProcessors = new[] { typeof(JsonProcessor), typeof(XmlProcessor) };
          });
        return nancyInternalConfig;
      }
    }

    /// <summary>
    /// Request Startup
    /// </summary>
    /// <param name="container">Ioc Container</param>
    /// <param name="pipelines">Nancy Pipelines</param>
    /// <param name="context">Nancy Context</param>
    protected override void RequestStartup(IContainer container, IPipelines pipelines, NancyContext context)
    {
      base.RequestStartup(container, pipelines, context);

      ThuriaCustomErrorHandler.Enable(pipelines, container.GetInstance<IResponseNegotiator>());
    }
  }
}

using System;
using System.Linq;
using System.Collections.Generic;

using Nancy;
using NSubstitute;
using StructureMap;
using Nancy.Testing;
using Nancy.Bootstrapper;

using Thuria.Zitidar.Core;

namespace Thuria.Helium.Akka.Nancy.Tests
{
  public class NancyTestBootstrapper : HeliumNancyBootstrapper
  {
    private readonly List<Tuple<Type, object, string>> objectsToInject = new List<Tuple<Type, object, string>>();

    public NancyTestBootstrapper(IContainer container)
        : base(container)
    {
    }

    protected override IEnumerable<ModuleRegistration> Modules
    {
      get
      {
        return base.Modules.Where(x => x.ModuleType != typeof(ConfigurableNancyModule));
      }
    }
    
    public void AddInstanceToContainer<T>(Type instanceType, T instance) where T : class
    {
      var instanceToInject = new Tuple<Type, object, string>(instanceType, instance, string.Empty);
      objectsToInject.Add(instanceToInject);
    }
    
    public void AddNamedInstanceToContainer<T>(Type instanceType, T instance, string instanceName) where T : class
    {
      var instanceToInject = new Tuple<Type, object, string>(instanceType, instance, instanceName);
      objectsToInject.Add(instanceToInject);
    }
    
    protected override void ConfigureApplicationContainer(IContainer existingContainer)
    {
      base.ConfigureApplicationContainer(existingContainer);
    
      existingContainer.Configure(x =>
      {
        x.For<IThuriaNancySettings>().Use(CreateFakeNancyServiceSettings());
        // x.For<IThuriaCorsSettings>().Use(this.CreateFakeCorsSettings());
        x.For<IThuriaLogger>().Use(CreateFakeNancyServiceLogger());
      });
    }
    
    protected override IContainer CreateRequestContainer(NancyContext context)
    {
      var requestContainer = base.CreateRequestContainer(context);
    
      foreach (var currentInstance in this.objectsToInject)
      {
        requestContainer.Configure(expression =>
        {
          if (string.IsNullOrWhiteSpace(currentInstance.Item3))
          {
            expression.For(currentInstance.Item1).Use(currentInstance.Item2);
          }
          else
          {
            expression.For(currentInstance.Item1).Use(currentInstance.Item2).Named(currentInstance.Item3);
          }
        });
      }
    
      return requestContainer;
    }
    
    private IThuriaNancySettings CreateFakeNancyServiceSettings()
    {
      return Substitute.For<IThuriaNancySettings>();
    }
    
    // private ISahlCorsSettings CreateFakeCorsSettings()
    // {
    //   return Substitute.For<ISahlCorsSettings>();
    // }
    
    private IThuriaLogger CreateFakeNancyServiceLogger()
    {
      return Substitute.For<IThuriaLogger>();
    }
  }
}

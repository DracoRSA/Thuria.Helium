using StructureMap;

using Thuria.Zitidar.Akka;
using Thuria.Helium.Akka.Nancy;
using Thuria.Zitidar.Core;

namespace Thuria.Helium.Akka.Structuremap
{
  /// <summary>
  /// Helium Akka IOC Registry
  /// </summary>
  public class HeliumAkkaIocRegistry : Registry
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public HeliumAkkaIocRegistry()
    {
      For<IThuriaActorSystem>().Use<HeliumActorSystem>()
                               .Named("Helium")
                               .Singleton();

      For<IThuriaStartable>().Use(context => context.GetInstance<IThuriaActorSystem>("Helium"));
      For<IThuriaStoppable>().Use(context => context.GetInstance<IThuriaActorSystem>("Helium"));

      For<HeliumRetrieveModule>().Use<HeliumRetrieveModule>()
                                 .Ctor<IThuriaActorSystem>("heliumActorSystem").IsNamedInstance("Helium");
    }
  }
}

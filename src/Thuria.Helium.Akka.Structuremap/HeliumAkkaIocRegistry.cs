using StructureMap;

using Thuria.Zitidar.Akka;
using Thuria.Helium.Akka.Nancy;

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
      For<HeliumRetrieveModule>().Use<HeliumRetrieveModule>()
                                 .Ctor<IThuriaActorSystem>("heliumActorSystem").IsNamedInstance("Helium");
    }
  }
}

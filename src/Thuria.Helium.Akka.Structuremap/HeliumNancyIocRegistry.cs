using StructureMap;

using Thuria.Zitidar.Akka;
using Thuria.Helium.Akka.Nancy;

namespace Thuria.Helium.Akka.Structuremap
{
  /// <summary>
  /// Helium Nancy Ioc Registry
  /// </summary>
  public class HeliumNancyIocRegistry : Registry
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public HeliumNancyIocRegistry()
    {
      For<HeliumRetrieveModule>().Use<HeliumRetrieveModule>()
                                 .Ctor<IThuriaActorSystem>("heliumActorSystem").IsNamedInstance("Helium");
      For<HeliumInsertModule>().Use<HeliumInsertModule>()
                               .Ctor<IThuriaActorSystem>("heliumActorSystem").IsNamedInstance("Helium");
    }
  }
}

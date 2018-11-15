using StructureMap;

using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Builders;

namespace Thuria.Helium.Akka.Structuremap
{
  /// <summary>
  /// Helium Thark IOC Registry
  /// </summary>
  public class HeliumTharkRegistry : Registry
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public HeliumTharkRegistry()
    {
      For<IDatabaseBuilder>().Use<DatabaseBuilder>();
    }
  }
}

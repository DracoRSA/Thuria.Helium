using StructureMap;

using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Builders;

namespace Thuria.Helium.Akka.Structuremap
{
  public class HeliumTharkRegistry : Registry
  {
    public HeliumTharkRegistry()
    {
      For<IDatabaseBuilder>().Use<DatabaseBuilder>();
    }
  }
}

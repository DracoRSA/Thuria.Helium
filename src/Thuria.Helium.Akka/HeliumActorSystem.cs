using Akka.DI.Core;

using Thuria.Zitidar.Core;
using Thuria.Helium.Akka.Actors;
using Thuria.Zitidar.Akka.Service;

namespace Thuria.Helium.Akka
{
  /// <summary>
  /// Helium Actor System
  /// </summary>
  public class HeliumActorSystem : ThuriaActorSystemBase
  {
    /// <summary>
    /// Helium Actor System constructor
    /// </summary>
    /// <param name="iocContainer"></param>
    public HeliumActorSystem(IThuriaIocContainer iocContainer) 
      : base(iocContainer)
    {
    }

    /// <inheritdoc />
    public override string Name { get; } = "Helium";

    /// <inheritdoc />
    public override void Start()
    {
      base.Start();

      ActorSystem.ActorOf(ActorSystem.DI().Props<HeliumRetrieveActor>(), "HeliumRetrieveAction");
    }
  }
}

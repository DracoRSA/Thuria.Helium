using Akka.Actor;
using Akka.Event;

namespace Thuria.Helium.Akka.Actors
{
  /// <summary>
  /// Helium Actor Base
  /// </summary>
  public abstract class HeliumActorBase : ReceiveActor
  {
    /// <summary>
    /// Helium Actor Base Constructor
    /// </summary>
    protected HeliumActorBase()
    {
      ActorLogger = Context.GetLogger();
    }

    /// <summary>
    /// Actor Logger
    /// </summary>
    protected ILoggingAdapter ActorLogger { get; }
  }
}

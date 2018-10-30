using Akka.Actor;

namespace Thuria.Helium.Akka
{
  /// <summary>
  /// Helium Stateful Message
  /// </summary>
  public interface IHeliumStatefulMessage
  {
    /// <summary>
    /// Original Actor that sent the message
    /// </summary>
    IActorRef OriginalSender { get; }

    /// <summary>
    /// Original Message
    /// </summary>
    object OriginalMessage { get; }
  }
}

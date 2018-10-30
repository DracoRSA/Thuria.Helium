using System;
using Akka.Actor;

namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Stateful Message base class
  /// </summary>
  public abstract class HeliumStatefulMessage : IHeliumStatefulMessage
  {
    /// <summary>
    /// Helium Stateful Message constructor
    /// </summary>
    /// <param name="originalSender">Original Actor that sent the message</param>
    /// <param name="originalMessage">Original Message</param>
    protected HeliumStatefulMessage(IActorRef originalSender, object originalMessage)
    {
      OriginalSender  = originalSender ?? throw new ArgumentNullException(nameof(originalSender));
      OriginalMessage = originalMessage;
    }

    /// <summary>
    /// Original Actor that sent the message
    /// </summary>
    public IActorRef OriginalSender { get; }

    /// <summary>
    /// Original Message
    /// </summary>
    public object OriginalMessage { get; }
  }
}

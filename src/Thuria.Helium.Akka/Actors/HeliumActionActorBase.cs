using Akka.Actor;
using Akka.Event;

using Thuria.Helium.Akka.Core;
using Thuria.Helium.Akka.Messages;

namespace Thuria.Helium.Akka.Actors
{
  /// <summary>
  /// Helium Action Actor Base
  /// </summary>
  public abstract class HeliumActionActorBase : HeliumActorBase
  {
    /// <summary>
    /// Helium Action Actor Base constructor
    /// </summary>
    /// <param name="heliumAction">Helium Action</param>
    protected HeliumActionActorBase(HeliumAction heliumAction)
    {
      HeliumAction = heliumAction;

      Become(ReadyToPerformAction);
    }

    /// <summary>
    /// Helium Action supported by this Actor
    /// </summary>
    protected HeliumAction HeliumAction { get; private set; }

    /// <summary>
    /// Original Sending Actor
    /// </summary>
    protected IActorRef OriginalSender { get; set; }

    /// <summary>
    /// Original Helium Action Message
    /// </summary>
    protected HeliumActionMessage HeliumActionMessage { get; set; }

    /// <summary>
    /// Make sre the actor is in a state to Perform the required action
    /// </summary>
    protected virtual void ReadyToPerformAction()
    {
      ActorLogger.Log(LogLevel.InfoLevel, $"Ready to perform Helium {HeliumAction} Action");

      Receive<HeliumActionMessage>(HandleHeliumAction, message => message.HeliumAction == HeliumAction);
    }

    /// <summary>
    /// Start the Helium Action Processing
    /// </summary>
    protected abstract void StartHeliumActionProcessing(HeliumActionMessage actionMessage);

    private void HandleHeliumAction(HeliumActionMessage actionMessage)
    {
      ActorLogger.Log(LogLevel.InfoLevel, $"Starting Helium Action : {actionMessage.HeliumAction}");

      OriginalSender      = Sender;
      HeliumActionMessage = actionMessage;

      StartHeliumActionProcessing(actionMessage);
    }
  }
}

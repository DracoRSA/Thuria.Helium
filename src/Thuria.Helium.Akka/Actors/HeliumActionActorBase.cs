using Akka.Actor;
using Akka.Event;

using Thuria.Helium.Core;
using Thuria.Helium.Akka.Core.Messages;

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
    /// Make sre the actor is in a state to Perform the required action
    /// </summary>
    protected virtual void ReadyToPerformAction()
    {
      ActorLogger.Log(LogLevel.InfoLevel, $"Helium Actor ready to process {HeliumAction} Action");

      Receive<HeliumActionMessage>(HandleHeliumAction, message => message.HeliumAction == HeliumAction);
    }

    /// <summary>
    /// Start the Helium Action Processing
    /// </summary>
    protected abstract void StartHeliumActionProcessing(HeliumActionMessage actionMessage);

    /// <summary>
    /// Unhandled message handler
    /// </summary>
    /// <param name="message"></param>
    protected override void Unhandled(object message)
    {
      ActorLogger.Log(LogLevel.WarningLevel, $"Unhandled message received -> {message}");
      base.Unhandled(message);
    }

    private void HandleHeliumAction(HeliumActionMessage actionMessage)
    {
      ActorLogger.Log(LogLevel.InfoLevel, $"Starting Helium Action : {actionMessage.HeliumAction}");
      StartHeliumActionProcessing(actionMessage);
    }
  }
}

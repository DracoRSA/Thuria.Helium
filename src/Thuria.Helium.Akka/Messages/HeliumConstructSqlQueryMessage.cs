using Akka.Actor;
using Thuria.Helium.Core;

namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Construct SQL Query Message
  /// </summary>
  public class HeliumConstructSqlQueryMessage : HeliumStatefulMessage
  {
    /// <summary>
    /// Helium Const SQL Query Message Constructor
    /// </summary>
    /// <param name="heliumAction">Helium Action</param>
    /// <param name="originalSender">Original Sender of the message</param>
    /// <param name="originalMessage">Original Message</param>
    public HeliumConstructSqlQueryMessage(HeliumAction heliumAction, IActorRef originalSender, object originalMessage)
      : base(originalSender, originalMessage)
    {
      HeliumAction = heliumAction;
    }

    /// <summary>
    /// Helium Action
    /// </summary>
    public HeliumAction HeliumAction { get; }
  }
}

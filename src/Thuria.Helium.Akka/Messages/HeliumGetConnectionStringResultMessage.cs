using Akka.Actor;

namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Get Connection String Result Message
  /// </summary>
  public class HeliumGetConnectionStringResultMessage : HeliumStatefulMessage, IHeliumStatefulResultMessage
  {
    /// <summary>
    /// Helium Get Connection String Result Message constructor
    /// </summary>
    /// <param name="connectionString">Connection String</param>
    /// <param name="originalSender">Original Actor that sent the message</param>
    /// <param name="originalMessage">Original Message</param>
    public HeliumGetConnectionStringResultMessage(string connectionString, IActorRef originalSender, object originalMessage)
      : base(originalSender, originalMessage)
    {
      ConnectionString = connectionString;
    }

    /// <summary>
    /// Connection String
    /// </summary>
    public string ConnectionString { get; }
  }
}

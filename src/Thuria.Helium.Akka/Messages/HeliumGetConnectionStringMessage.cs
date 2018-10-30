using Akka.Actor;

namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Get Connection String message
  /// </summary>
  public class HeliumGetConnectionStringMessage : HeliumStatefulMessage
  {
    /// <summary>
    /// Helium Get Connection String message constructor
    /// </summary>
    /// <param name="dbContextName">Database Context name</param>
    /// <param name="originalSender">Original Actor that sent the Message</param>
    /// <param name="originalMessage">Original Message</param>
    public HeliumGetConnectionStringMessage(string dbContextName, IActorRef originalSender, object originalMessage)
      : base(originalSender, originalMessage)
    {
      DbContextName = dbContextName;
    }

    /// <summary>
    /// Database Context Name
    /// </summary>
    public string DbContextName { get; }
  }
}

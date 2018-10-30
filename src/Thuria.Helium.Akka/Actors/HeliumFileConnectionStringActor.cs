using System;

using Akka.Actor;
using Akka.Event;

using Thuria.Zitidar.Core;
using Thuria.Helium.Akka.Messages;

namespace Thuria.Helium.Akka.Actors
{
  /// <summary>
  /// Helium File Connection String Actor
  /// </summary>
  public class HeliumFileConnectionStringActor : HeliumActorBase
  {
    /// <summary>
    /// Helium File Connection String Actor constructor
    /// </summary>
    /// <param name="databaseSettings"></param>
    public HeliumFileConnectionStringActor(IThuriaDatabaseSettings databaseSettings)
    {
      if (databaseSettings == null)
      {
        throw new ArgumentNullException(nameof(databaseSettings));
      }

      Receive<HeliumGetConnectionStringMessage>(message =>
        {
          var connectionString = databaseSettings.GetConnectionString(message.DbContextName);
          ActorLogger.Log(LogLevel.InfoLevel, $"Retrieved Connection String Context: {message.DbContextName} String: {connectionString}");

          var resultMessage = new HeliumGetConnectionStringResultMessage(connectionString);
          Sender.Tell(resultMessage);
        });
    }
  }
}
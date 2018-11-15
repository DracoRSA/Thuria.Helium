using System;
using Akka.Actor;
using Thuria.Helium.Core;

namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Execute SQL Query Message
  /// </summary>
  public class HeliumExecuteSqlQueryMessage : HeliumStatefulMessage
  {
    /// <summary>
    /// Helium Execute SQL Query Message Constructor
    /// </summary>
    /// <param name="dbContextName">Database Context Name</param>
    /// <param name="heliumAction">Helium Action</param>
    /// <param name="sqlQuery">SQL Query to be executed</param>
    /// <param name="originalSender"></param>
    /// <param name="originalMessage"></param>
    public HeliumExecuteSqlQueryMessage(string dbContextName, HeliumAction heliumAction, string sqlQuery, IActorRef originalSender, object originalMessage)
      : base(originalSender, originalMessage)
    {
      if (string.IsNullOrWhiteSpace(dbContextName)) { throw new ArgumentNullException(nameof(dbContextName)); }
      if (string.IsNullOrWhiteSpace(sqlQuery)) { throw new ArgumentNullException(nameof(sqlQuery)); }

      DatabaseContextName = dbContextName;
      HeliumAction        = heliumAction;
      SqlQuery            = sqlQuery;
    }

    /// <summary>
    /// Database Context Name
    /// </summary>
    public string DatabaseContextName { get; }

    /// <summary>
    /// Helium Action
    /// </summary>
    public HeliumAction HeliumAction { get; }

    /// <summary>
    /// SQL Query to be executed
    /// </summary>
    public string SqlQuery { get; }
  }
}

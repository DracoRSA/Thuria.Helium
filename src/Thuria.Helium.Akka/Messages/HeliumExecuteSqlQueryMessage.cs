using System;

using Thuria.Zitidar.Akka;
using Thuria.Helium.Akka.Core;

namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Execute SQL Query Message
  /// </summary>
  public class HeliumExecuteSqlQueryMessage : IThuriaActorMessage
  {
    /// <summary>
    /// Helium Execute SQL Query Message Constructor
    /// </summary>
    /// <param name="messageId">Message ID</param>
    /// <param name="dbContextName">Database Context Name</param>
    /// <param name="heliumAction">Helium Action</param>
    /// <param name="sqlQuery">SQL Query to be executed</param>
    public HeliumExecuteSqlQueryMessage(Guid messageId, string dbContextName, HeliumAction heliumAction, string sqlQuery)
    {
      if (string.IsNullOrWhiteSpace(dbContextName)) { throw new ArgumentNullException(nameof(dbContextName)); }
      if (string.IsNullOrWhiteSpace(sqlQuery)) { throw new ArgumentNullException(nameof(sqlQuery)); }

      Id                  = messageId;
      DatabaseContextName = dbContextName;
      HeliumAction        = heliumAction;
      SqlQuery            = sqlQuery;
    }

    /// <summary>
    /// Message ID
    /// </summary>
    public Guid Id { get; }

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

using Akka.Actor;
using Thuria.Helium.Core;

namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Construct SQL Query Result Message
  /// </summary>
  public class HeliumConstructSqlQueryResultMessage : HeliumStatefulMessage
  {
    /// <summary>
    /// Helium Construct SQL Query Result Message
    /// </summary>
    /// <param name="heliumAction">Helium Action</param>
    /// <param name="sqlQuery">Created SQL Query</param>
    /// <param name="originalSender"></param>
    /// <param name="originalMessage"></param>
    public HeliumConstructSqlQueryResultMessage(HeliumAction heliumAction, string sqlQuery, IActorRef originalSender, object originalMessage)
      : base(originalSender, originalMessage)
    {
      HeliumAction = heliumAction;
      SqlQuery     = sqlQuery;
    }

    /// <summary>
    /// Helium Action
    /// </summary>
    public HeliumAction HeliumAction { get; }

    /// <summary>
    /// SQL Query
    /// </summary>
    public string SqlQuery { get; }
  }
}

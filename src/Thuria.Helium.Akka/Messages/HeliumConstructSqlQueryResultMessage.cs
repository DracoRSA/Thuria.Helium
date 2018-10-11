using System;

using Thuria.Zitidar.Akka;
using Thuria.Helium.Akka.Core;

namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Construct SQL Query Result Message
  /// </summary>
  public class HeliumConstructSqlQueryResultMessage : IThuriaActorMessage
  {
    /// <summary>
    /// Helium Construct SQL Query Result Message
    /// </summary>
    /// <param name="messageId">Unique Message ID</param>
    /// <param name="heliumAction">Helium Action</param>
    /// <param name="dataModel">Input data Model</param>
    /// <param name="sqlQuery">Created SQL Query</param>
    public HeliumConstructSqlQueryResultMessage(Guid messageId, HeliumAction heliumAction, object dataModel, string sqlQuery)
    {
      Id           = messageId;
      HeliumAction = heliumAction;
      DataModel    = dataModel;
      SqlQuery     = sqlQuery;
    }

    /// <summary>
    /// Actor Message ID
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Helium Action
    /// </summary>
    public HeliumAction HeliumAction { get; }

    /// <summary>
    /// Input Data Model
    /// </summary>
    public object DataModel { get; }

    /// <summary>
    /// SQL Query
    /// </summary>
    public string SqlQuery { get; }
  }
}

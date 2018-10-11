using System;

using Thuria.Zitidar.Akka;
using Thuria.Helium.Akka.Core;

namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Construct SQL Query Message
  /// </summary>
  public class HeliumConstructSqlQueryMessage : IThuriaActorMessage
  {
    /// <summary>
    /// Helium Const SQL Query Message Constructor
    /// </summary>
    /// <param name="messageId">Unique Message ID</param>
    /// <param name="heliumAction">Helium Action</param>
    /// <param name="dataModel">Input Data Model</param>
    public HeliumConstructSqlQueryMessage(Guid messageId, HeliumAction heliumAction, object dataModel)
    {
      if (messageId == Guid.Empty) { throw new ArgumentNullException(nameof(messageId)); }

      Id           = messageId;
      HeliumAction = heliumAction;
      DataModel    = dataModel ?? throw new ArgumentNullException(nameof(dataModel));
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
    /// Data Model
    /// </summary>
    public object DataModel { get; }
  }
}

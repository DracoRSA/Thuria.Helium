using System;
using System.Collections.Generic;

using Thuria.Zitidar.Akka;
using Thuria.Helium.Akka.Core;

namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Execute SQL Query Result message
  /// </summary>
  public class HeliumExecuteSqlQueryResultMessage : IThuriaActorMessage
  {
    /// <summary>
    /// Helium Execute SQL Query Result message constructor
    /// </summary>
    /// <param name="messageId">Message ID</param>
    /// <param name="heliumAction">Helium Action</param>
    /// <param name="actionResult">Helium Action Result</param>
    /// <param name="resultData">Result Data (Optional)</param>
    /// <param name="errorDetail">Error Detail (Optional)</param>
    public HeliumExecuteSqlQueryResultMessage(Guid messageId, HeliumAction heliumAction, HeliumActionResult actionResult, 
                                              IEnumerable<object> resultData = null, object errorDetail = null)
    {
      Id           = messageId;
      HeliumAction = heliumAction;
      ActionResult = actionResult;
      ResultData   = resultData;
      ErrorDetail  = errorDetail;
    }

    /// <summary>
    /// Message ID
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Helium Action performed
    /// </summary>
    public HeliumAction HeliumAction { get; }

    /// <summary>
    /// Helium Action Result
    /// </summary>
    public HeliumActionResult ActionResult { get; }

    /// <summary>
    /// Result Data
    /// </summary>
    public IEnumerable<object> ResultData { get; }

    /// <summary>
    /// Error Detail
    /// </summary>
    public object ErrorDetail { get; }
  }
}

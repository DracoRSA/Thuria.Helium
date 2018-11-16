using System.Collections.Generic;
using Thuria.Helium.Core;

namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Execute SQL Query Result message
  /// </summary>
  public class HeliumExecuteSqlQueryResultMessage : HeliumStatefulMessage
  {
    /// <summary>
    /// Helium Execute SQL Query Result message constructor
    /// </summary>
    /// <param name="heliumAction">Helium Action</param>
    /// <param name="actionResult">Helium Action Result</param>
    /// <param name="resultData">Result Data (Optional)</param>
    /// <param name="errorDetail">Error Detail (Optional)</param>
    public HeliumExecuteSqlQueryResultMessage(HeliumAction heliumAction, HeliumActionResult actionResult, 
                                              IEnumerable<object> resultData = null, object errorDetail = null)
    {
      HeliumAction = heliumAction;
      ActionResult = actionResult;
      ResultData   = resultData;
      ErrorDetail  = errorDetail;
    }

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

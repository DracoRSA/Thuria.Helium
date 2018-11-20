using System.Collections.Generic;
using Thuria.Helium.Core;

namespace Thuria.Helium.Akka.Core.Messages
{
  /// <summary>
  /// Helium Action Result Message
  /// </summary>
  public class HeliumActionResultMessage
  {
    /// <summary>
    /// Helium Action Result Message Constructor
    /// </summary>
    /// <param name="heliumActionResult">Helium Action Result</param>
    /// <param name="resultData">Result Data (Optional)</param>
    /// <param name="errorDetail">Error Details (Optional)</param>
    public HeliumActionResultMessage(HeliumActionResult heliumActionResult, IEnumerable<object> resultData = null, string errorDetail = null)
    {
      HeliumActionResult = heliumActionResult;
      ResultData         = resultData;
      ErrorDetail        = errorDetail;
    }

    /// <summary>
    /// Helium Action Result
    /// </summary>
    public HeliumActionResult HeliumActionResult { get; }
    
    /// <summary>
    /// Helium Action Result Data
    /// </summary>
    public IEnumerable<object> ResultData { get; }

    /// <summary>
    /// Error Detail (If any)
    /// </summary>
    public string ErrorDetail { get; }
  }
}

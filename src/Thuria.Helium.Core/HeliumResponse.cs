using System.Collections.Generic;

namespace Thuria.Helium.Core
{
  /// <summary>
  /// Helium Response
  /// </summary>
  public class HeliumResponse
  {
    /// <summary>
    /// Helium Action Result
    /// </summary>
    public HeliumActionResult ActionResult { get; set; }

    /// <summary>
    /// Result Data
    /// </summary>
    public IEnumerable<object> ResultData { get; set; }
  }
}

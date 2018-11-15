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
    public HeliumActionResultMessage(HeliumActionResult heliumActionResult)
    {
      HeliumActionResult = heliumActionResult;
    }

    /// <summary>
    /// Helium Action Result
    /// </summary>
    public HeliumActionResult HeliumActionResult { get; }
  }
}

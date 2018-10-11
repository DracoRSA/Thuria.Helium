using Thuria.Helium.Akka.Core;

namespace Thuria.Helium.Akka.Messages
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

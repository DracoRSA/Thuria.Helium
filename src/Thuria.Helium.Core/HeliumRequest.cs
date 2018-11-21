namespace Thuria.Helium.Core
{
  /// <summary>
  /// Helium Request Model
  /// </summary>
  public class HeliumRequest
  {
    /// <summary>
    /// Helium Action
    /// </summary>
    public HeliumAction Action { get; set; }

    /// <summary>
    /// Request Data
    /// </summary>
    public object RequestData { get; set; }
  }
}

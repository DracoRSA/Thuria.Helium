namespace Thuria.Helium.Core
{
  /// <summary>
  /// Helium Request Model
  /// </summary>
  public class HeliumRequestModel
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

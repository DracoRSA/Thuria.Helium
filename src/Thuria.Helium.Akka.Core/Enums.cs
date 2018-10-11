namespace Thuria.Helium.Akka.Core
{
  /// <summary>
  /// Helium Action enumeration
  /// </summary>
  public enum HeliumAction
  {
    /// <summary>
    /// Perform no Action
    /// </summary>
    None,

    /// <summary>
    /// Perform a retrieve
    /// </summary>
    Retrieve
  }

  /// <summary>
  /// Helium Action Result enumeration
  /// </summary>
  public enum HeliumActionResult
  {
    /// <summary>
    /// Result is Unknown
    /// </summary>
    Unknown,

    /// <summary>
    /// Successful Action
    /// </summary>
    Success,

    /// <summary>
    /// Action resulted in warnings issued
    /// </summary>
    Warning,

    /// <summary>
    /// Action resulted in errors
    /// </summary>
    Error
  }
}

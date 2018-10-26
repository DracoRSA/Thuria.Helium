namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Get Connection String Result Message
  /// </summary>
  public class HeliumGetConnectionStringResultMessage
  {
    /// <summary>
    /// Helium Get Connection String Result Message constructor
    /// </summary>
    /// <param name="connectionString">Connection String</param>
    public HeliumGetConnectionStringResultMessage(string connectionString)
    {
      ConnectionString = connectionString;
    }

    /// <summary>
    /// Connection String
    /// </summary>
    public string ConnectionString { get; }
  }
}

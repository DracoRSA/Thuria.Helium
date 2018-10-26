namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Get Connection String message
  /// </summary>
  public class HeliumGetConnectionStringMessage
  {
    /// <summary>
    /// Helium Get Connection String message constructor
    /// </summary>
    /// <param name="dbContextName">Database Context name</param>
    public HeliumGetConnectionStringMessage(string dbContextName)
    {
      DbContextName = dbContextName;
    }

    /// <summary>
    /// Database Context Name
    /// </summary>
    public string DbContextName { get; }
  }
}

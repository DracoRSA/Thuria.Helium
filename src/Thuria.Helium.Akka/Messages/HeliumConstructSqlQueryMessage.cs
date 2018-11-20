using Thuria.Helium.Core;

namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Construct SQL Query Message
  /// </summary>
  public class HeliumConstructSqlQueryMessage : HeliumStatefulMessage
  {
    /// <summary>
    /// Helium Const SQL Query Message Constructor
    /// </summary>
    /// <param name="heliumAction">Helium Action</param>
    /// <param name="dataModel">Data Model</param>
    public HeliumConstructSqlQueryMessage(HeliumAction heliumAction, object dataModel)
    {
      HeliumAction = heliumAction;
      DataModel    = dataModel;
    }

    /// <summary>
    /// Helium Action
    /// </summary>
    public HeliumAction HeliumAction { get; }

    /// <summary>
    /// Data Model
    /// </summary>
    public object DataModel { get; }
  }
}

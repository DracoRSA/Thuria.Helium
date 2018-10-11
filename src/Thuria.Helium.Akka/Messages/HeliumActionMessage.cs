using Thuria.Helium.Akka.Core;

namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Action Message
  /// </summary>
  public class HeliumActionMessage
  {
    /// <summary>
    /// Helium Action Message Constructor
    /// </summary>
    /// <param name="heliumAction">Helium Action to perform</param>
    /// <param name="dataModel">Action Input Data Model</param>
    public HeliumActionMessage(HeliumAction heliumAction, object dataModel)
    {
      HeliumAction = heliumAction;
      DataModel    = dataModel;
    }

    /// <summary>
    /// Helium Action to be performed
    /// </summary>
    public HeliumAction HeliumAction { get; }

    /// <summary>
    /// Input Data Model
    /// </summary>
    public object DataModel { get; }
  }
}

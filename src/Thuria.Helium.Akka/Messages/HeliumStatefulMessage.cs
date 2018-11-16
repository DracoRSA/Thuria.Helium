using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Thuria.Helium.Akka.Messages
{
  /// <summary>
  /// Helium Stateful Message base class
  /// </summary>
  public abstract class HeliumStatefulMessage : IHeliumStatefulMessage
  {
    /// <inheritdoc />
    public IDictionary<string, object> MessageStateData { get; } = new ConcurrentDictionary<string, object>();

    /// <inheritdoc />
    public void AddStateData(string dataKey , object stateData)
    {
      MessageStateData.Add(dataKey, stateData);
    }

    /// <inheritdoc />
    public void AddStateData(IDictionary<string, object> stateData)
    {
      foreach (var currentStateData in stateData)
      {
        MessageStateData.Add(currentStateData.Key, currentStateData.Value);
      }
    }
  }
}

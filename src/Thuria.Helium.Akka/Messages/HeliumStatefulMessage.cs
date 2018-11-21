using System;
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
      if (string.IsNullOrWhiteSpace(dataKey)) { throw new ArgumentNullException(nameof(dataKey)); }
      if (stateData == null) { throw new ArgumentNullException(nameof(stateData)); }

      MessageStateData.Add(dataKey, stateData);
    }

    /// <inheritdoc />
    public void AddStateData(IDictionary<string, object> stateDataList)
    {
      if (stateDataList == null) { throw new ArgumentNullException(nameof(stateDataList)); }

      foreach (var currentStateData in stateDataList)
      {
        MessageStateData.Add(currentStateData.Key, currentStateData.Value);
      }
    }
  }
}
 
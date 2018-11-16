using System.Collections.Generic;

namespace Thuria.Helium.Akka
{
  /// <summary>
  /// Helium Stateful Message
  /// </summary>
  public interface IHeliumStatefulMessage
  {
    /// <summary>
    /// Message State Data
    /// </summary>
    IDictionary<string, object> MessageStateData { get; }

    /// <summary>
    /// Add Message State Data
    /// </summary>
    /// <param name="dataKey">Message State Data Key</param>
    /// <param name="stateData">Message State Data</param>
    void AddStateData(string dataKey, object stateData);

    /// <summary>
    /// Add Message State Data
    /// </summary>
    /// <param name="stateData">Dictionary of State Data to add the current message State Data</param>
    void AddStateData(IDictionary<string, object> stateData);
  }
}

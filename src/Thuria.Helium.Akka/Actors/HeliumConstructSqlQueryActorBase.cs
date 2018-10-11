using System;
using Thuria.Helium.Akka.Core;

namespace Thuria.Helium.Akka.Actors
{
  /// <summary>
  /// Helium Construct SQL Query Base Actor
  /// </summary>
  public abstract class HeliumConstructSqlQueryActorBase : HeliumActorBase
  {
    /// <summary>
    /// Retrieve the Where Conditions for a given Data Model
    /// </summary>
    /// <param name="heliumAction">Helium Action</param>
    /// <param name="dataModel">Data Model</param>
    /// <returns>A string representing the where clause</returns>
    protected string GetWhereConditionsForDataModel(HeliumAction heliumAction, object dataModel)
    {
      throw new NotImplementedException();
    }
  }
}

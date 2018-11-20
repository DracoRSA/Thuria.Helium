using System;

using Thuria.Thark.DataModel;
using Thuria.Zitidar.Extensions;
using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Builders;

namespace Thuria.Helium.Akka.Actors
{
  /// <summary>
  /// Helium Construct SQL Query Base Actor
  /// </summary>
  public abstract class HeliumConstructSqlQueryActorBase : HeliumActorBase
  {
    private readonly IConditionBuilder _conditionBuilder;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="conditionBuilder">Condition Builder</param>
    protected HeliumConstructSqlQueryActorBase(IConditionBuilder conditionBuilder)
    {
      _conditionBuilder = conditionBuilder ?? throw new ArgumentNullException(nameof(conditionBuilder));
    }

    /// <summary>
    /// Retrieve the Where Conditions for a given Data Model
    /// </summary>
    /// <param name="tharkAction">Thark Action</param>
    /// <param name="dataModel">Data Model</param>
    /// <returns>A string representing the where clause</returns>
    protected string GetWhereConditionsForDataModel(TharkAction tharkAction, object dataModel)
    {
      var conditionCount      = 0;
      var tableName           = dataModel.GetThuriaDataModelTableName();
      var dataModelConditions = dataModel.GetThuriaDataModelConditions(tharkAction);

      foreach (var currentCondition in dataModelConditions)
      {
        var propertyValue = currentCondition.Value;
        if (propertyValue == null || propertyValue.Equals(propertyValue.GetType().GetDefaultData())) { continue; }

        if (conditionCount > 0)
        {
          _conditionBuilder.WithAnd();
        }

        _conditionBuilder.WithCondition(tableName, currentCondition.ColumnName, EqualityOperators.Equals, propertyValue);
        conditionCount++;
      }

      return _conditionBuilder.Build();
    }
  }
}

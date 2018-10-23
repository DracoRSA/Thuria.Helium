using Thuria.Thark.DataModel;
using Thuria.Zitidar.Extensions;
using Thuria.Thark.Core.Statement;
using Thuria.Thark.StatementBuilder.Builders;

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
    /// <param name="tharkAction">Thark Action</param>
    /// <param name="dataModel">Data Model</param>
    /// <returns>A string representing the where clause</returns>
    protected string GetWhereConditionsForDataModel(TharkAction tharkAction, object dataModel)
    {
      var conditionCount      = 0;
      var builder             = ConditionBuilder.Create();
      var tableName           = dataModel.GetThuriaDataModelTableName();
      var dataModelConditions = dataModel.GetThuriaDataModelConditions(tharkAction);

      foreach (var currentCondition in dataModelConditions)
      {
        var propertyValue = currentCondition.Value;
        if (propertyValue == null || propertyValue.Equals(propertyValue.GetType().GetDefaultData())) { continue; }

        if (conditionCount > 0)
        {
          builder.WithAnd();
        }

        builder.WithCondition(tableName, currentCondition.ColumnName, EqualityOperators.Equals, propertyValue);
        conditionCount++;
      }

      return builder.Build();
    }
  }
}

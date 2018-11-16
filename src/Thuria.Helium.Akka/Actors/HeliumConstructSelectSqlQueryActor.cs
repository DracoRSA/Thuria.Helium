using System;

using Akka.Event;

using Thuria.Helium.Core;
using Thuria.Thark.DataModel;
using Thuria.Helium.Akka.Messages;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.StatementBuilder.Models;

namespace Thuria.Helium.Akka.Actors
{
  /// <summary>
  /// Helium Construct Select SQL Query Actor
  /// </summary>
  public class HeliumConstructSelectSqlQueryActor : HeliumConstructSqlQueryActorBase
  {
    private readonly ISelectStatementBuilder _selectStatementBuilder;

    /// <summary>
    /// Helium Construct Select SQL Query Actor Constructor
    /// </summary>
    public HeliumConstructSelectSqlQueryActor(ISelectStatementBuilder selectStatementBuilder, IConditionBuilder conditionBuilder)
      : base(conditionBuilder)
    {
      _selectStatementBuilder = selectStatementBuilder ?? throw new ArgumentNullException(nameof(selectStatementBuilder));

      Receive<HeliumConstructSqlQueryMessage>(HandleConstructSqlQueryMessage, message => message.HeliumAction == HeliumAction.Retrieve);
    }

    private void HandleConstructSqlQueryMessage(HeliumConstructSqlQueryMessage requestMessage)
    {
      ActorLogger.Log(LogLevel.InfoLevel, $"Constructing {requestMessage.HeliumAction} SQL Query for {requestMessage.DataModel.GetType().Name}");
      
      var sqlQuery      = ConstructSelectStatement(requestMessage.DataModel);
      var resultMessage = new HeliumConstructSqlQueryResultMessage(requestMessage.HeliumAction, sqlQuery);
      resultMessage.AddStateData(requestMessage.MessageStateData);
      
      Sender.Tell(resultMessage, Self);
    }

    private string ConstructSelectStatement(object dataModel)
    {
      var dataModelTable   = dataModel.GetThuriaDataModelTableName();
      var dataModelColumns = dataModel.GetThuriaDataModelColumns(TharkAction.Retrieve);
      var whereCondition   = GetWhereConditionsForDataModel(TharkAction.Retrieve, dataModel);

      var selectStatementBuilder = _selectStatementBuilder.WithTable(dataModelTable);

      foreach (var currentColumn in dataModelColumns)
      {
        var columnModel = new ColumnModel(dataModelTable, currentColumn.ColumnName, currentColumn.Alias);
        selectStatementBuilder.WithColumn(columnModel);
      }
      
      if (!string.IsNullOrWhiteSpace(whereCondition))
      {
        selectStatementBuilder.WithWhereCondition(whereCondition);
      }
      
      return selectStatementBuilder.Build();
    }
  }
}

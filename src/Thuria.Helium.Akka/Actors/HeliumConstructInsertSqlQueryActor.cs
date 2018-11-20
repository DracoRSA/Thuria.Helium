using System;

using Akka.Event;

using Thuria.Helium.Core;
using Thuria.Thark.DataModel;
using Thuria.Zitidar.Extensions;
using Thuria.Helium.Akka.Messages;
using Thuria.Thark.Core.Statement.Builders;

namespace Thuria.Helium.Akka.Actors
{
  /// <summary>
  /// Helium Construct Insert SQL Query Actor
  /// </summary>
  public class HeliumConstructInsertSqlQueryActor : HeliumConstructSqlQueryActorBase
  {
    private readonly IInsertStatementBuilder _statementBuilder;

    /// <summary>
    /// Helium Construct Select SQL Query Actor Constructor
    /// </summary>
    public HeliumConstructInsertSqlQueryActor(IInsertStatementBuilder insertStatementBuilder, IConditionBuilder conditionBuilder)
      : base(conditionBuilder)
    {
      _statementBuilder = insertStatementBuilder ?? throw new ArgumentNullException(nameof(insertStatementBuilder));

      Receive<HeliumConstructSqlQueryMessage>(HandleConstructSqlQueryMessage, message => message.HeliumAction == HeliumAction.Insert);
    }

    private void HandleConstructSqlQueryMessage(HeliumConstructSqlQueryMessage requestMessage)
    {
      ActorLogger.Log(LogLevel.InfoLevel, $"Constructing {requestMessage.HeliumAction} SQL Query for {requestMessage.DataModel.GetType().Name}");
      
      var sqlQuery      = ConstructInsertStatement(requestMessage.DataModel);
      var resultMessage = new HeliumConstructSqlQueryResultMessage(requestMessage.HeliumAction, sqlQuery);
      resultMessage.AddStateData(requestMessage.MessageStateData);
      
      Sender.Tell(resultMessage, Self);
    }

    private string ConstructInsertStatement(object dataModel)
    {
      var dataModelTable   = dataModel.GetThuriaDataModelTableName();
      var dataModelColumns = dataModel.GetThuriaDataModelColumns(TharkAction.Insert);
      var statementBuilder = _statementBuilder.WithTable(dataModelTable); 

      foreach (var currentColumn in dataModelColumns)
      {
        statementBuilder.WithColumn(currentColumn.ColumnName, dataModel.GetPropertyValue(currentColumn.PropertyName));
      }
      
      return statementBuilder.Build();
    }
  }
}

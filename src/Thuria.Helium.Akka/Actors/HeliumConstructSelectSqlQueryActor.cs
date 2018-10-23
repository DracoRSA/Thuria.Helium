using Akka.Event;

using Thuria.Thark.DataModel;
using Thuria.Helium.Akka.Core;
using Thuria.Helium.Akka.Messages;
using Thuria.Thark.StatementBuilder.Models;
using Thuria.Thark.StatementBuilder.Builders;

namespace Thuria.Helium.Akka.Actors
{
  /// <summary>
  /// Helium Construct Select SQL Query Actor
  /// </summary>
  public class HeliumConstructSelectSqlQueryActor : HeliumConstructSqlQueryActorBase
  {
    /// <summary>
    /// Helium Construct Select SQL Query Actor Constructor
    /// </summary>
    public HeliumConstructSelectSqlQueryActor()
    {
      Receive<HeliumConstructSqlQueryMessage>(HandleConstructSqlQueryMessage, message => message.HeliumAction == HeliumAction.Retrieve);
    }

    private void HandleConstructSqlQueryMessage(HeliumConstructSqlQueryMessage requestMessage)
    {
      ActorLogger.Log(LogLevel.InfoLevel, $"Constructing {requestMessage.HeliumAction} SQL Query for {requestMessage.DataModel.GetType().Name}");

      var sqlQuery      = ConstructSelectStatement(requestMessage.DataModel);
      var resultMessage = new HeliumConstructSqlQueryResultMessage(requestMessage.Id, requestMessage.HeliumAction, requestMessage.DataModel, sqlQuery);

      Sender.Tell(resultMessage, null);
    }

    private string ConstructSelectStatement(object dataModel)
    {
      var dataModelTable   = dataModel.GetThuriaDataModelTableName();
      var dataModelColumns = dataModel.GetThuriaDataModelColumns(TharkAction.Retrieve);
      var whereCondition   = this.GetWhereConditionsForDataModel(TharkAction.Retrieve, dataModel);

      var selectStatementBuilder = SelectStatementBuilder.Create().WithTable(dataModelTable);

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

using Akka.Event;

using Thuria.Thark.DataModel;
using Thuria.Helium.Akka.Core;
using Thuria.Zitidar.Extensions;
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
      var dataModel = requestMessage.OriginalMessage.GetPropertyValue("DataModel");

      ActorLogger.Log(LogLevel.InfoLevel, $"Constructing {requestMessage.HeliumAction} SQL Query for {dataModel.GetType().Name}");

      var sqlQuery      = ConstructSelectStatement(dataModel);
      var resultMessage = new HeliumConstructSqlQueryResultMessage(requestMessage.HeliumAction, sqlQuery, requestMessage.OriginalSender, requestMessage.OriginalMessage);

      Sender.Tell(resultMessage, null);
    }

    private string ConstructSelectStatement(object dataModel)
    {
      var dataModelTable   = dataModel.GetThuriaDataModelTableName();
      var dataModelColumns = dataModel.GetThuriaDataModelColumns(TharkAction.Retrieve);
      var whereCondition   = GetWhereConditionsForDataModel(TharkAction.Retrieve, dataModel);

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

using System;
using System.Collections.Generic;

using Akka.Actor;
using Akka.DI.Core;

using Thuria.Helium.Core;
using Thuria.Thark.Core.Statement;
using Thuria.Helium.Akka.Messages;
using Thuria.Thark.Core.DataAccess;

namespace Thuria.Helium.Akka.Actors
{
  /// <summary>
  /// Helium Execute SQL Query ACtor
  /// </summary>
  public class HeliumExecuteSqlQueryActor : ReceiveActor
  {
    private readonly IDatabaseBuilder _databaseBuilder;
    private readonly IActorRef _connectionStringActor;

    /// <summary>
    /// Helium Execute SQL Query Actor constructor
    /// </summary>
    public HeliumExecuteSqlQueryActor(IDatabaseBuilder databaseBuilder)
    {
      _databaseBuilder       = databaseBuilder ?? throw new ArgumentNullException(nameof(databaseBuilder));
      _connectionStringActor = Context.ActorOf(Context.DI().Props<HeliumFileConnectionStringActor>());

      Receive<HeliumExecuteSqlQueryMessage>(message => HandleExecuteSqlQuery(message));
      Receive<HeliumGetConnectionStringResultMessage>(message => HandleConnectionStringResult(message));
    }

    private void HandleExecuteSqlQuery(HeliumExecuteSqlQueryMessage sqlQueryMessage)
    {
      var getConnectionStringMessage = new HeliumGetConnectionStringMessage(sqlQueryMessage.DatabaseContextName);
      getConnectionStringMessage.AddStateData(sqlQueryMessage.MessageStateData);

      getConnectionStringMessage.AddStateData("Sender", Sender);
      getConnectionStringMessage.AddStateData("HeliumAction", sqlQueryMessage.HeliumAction);
      getConnectionStringMessage.AddStateData("SqlQuery", sqlQueryMessage.SqlQuery);

      _connectionStringActor.Tell(getConnectionStringMessage);
    }

    private void HandleConnectionStringResult(HeliumGetConnectionStringResultMessage resultMessage)
    {
      var heliumAction   = (HeliumAction)ExtractMessageStateData(resultMessage.MessageStateData, "HeliumAction");
      var originalSender = (IActorRef)ExtractMessageStateData(resultMessage.MessageStateData, "Sender");
      var sqlQuery       = (string)ExtractMessageStateData(resultMessage.MessageStateData, "SqlQuery");

      var heliumResult               = HeliumActionResult.Success;
      var errorDetail                = string.Empty;
      IEnumerable<object> resultData = null;

      try
      {
        switch (heliumAction)
        {
          case HeliumAction.Retrieve:
            resultData = ExecuteSelectSqlQuery(resultMessage.ConnectionString, sqlQuery);
            break;

          case HeliumAction.Insert:
            var rowsAffected = ExecuteSqlQuery(resultMessage.ConnectionString, sqlQuery);
            if (rowsAffected <= 0)
            {
              heliumResult = HeliumActionResult.Warning;
              errorDetail  = "No rows affected by Insert Action";
            }
            break;

          default:
            throw new Exception($"Helium Action [{heliumAction}] not currently supported");
        }

      }
      catch (Exception runtimeException)
      {
        heliumResult = HeliumActionResult.Error;
        errorDetail = runtimeException.ToString();
      }
      finally
      {
        SendResultMessage(originalSender, heliumAction, heliumResult, resultData, errorDetail, resultMessage.MessageStateData);
      }
    }

    private IEnumerable<object> ExecuteSelectSqlQuery(string connectionString, string sqlQuery)
    {
      using (var databaseContext = _databaseBuilder.WithDatabaseProviderType(DatabaseProviderType.SqlServer)
                                                   .WithConnectionString(connectionString)
                                                   .BuildReadonly())
      {
        return databaseContext.Select<object>(sqlQuery);
      }
    }

    private int ExecuteSqlQuery(string connectionString, string sqlQuery)
    {
      using (var databaseContext = _databaseBuilder.WithDatabaseProviderType(DatabaseProviderType.SqlServer)
                                                   .WithConnectionString(connectionString)
                                                   .BuildReadonly())
      {
        return databaseContext.ExecuteSqlStatement(sqlQuery);
      }
    }

    private void SendResultMessage(IActorRef originalSender, HeliumAction heliumAction, 
                                   HeliumActionResult heliumActionResult, IEnumerable<object> resultData = null, object errorDetail = null,
                                   IDictionary<string, object> messageStateData = null)
    {
      var executeSqlQueryResultMessage = new HeliumExecuteSqlQueryResultMessage(heliumAction, heliumActionResult, resultData, errorDetail);
      executeSqlQueryResultMessage.AddStateData(messageStateData);

      originalSender.Tell(executeSqlQueryResultMessage);
    }

    private object ExtractMessageStateData(IDictionary<string, object> messageData, string dataKey, bool isRequired = true)
    {
      if (!messageData.ContainsKey(dataKey) && isRequired)
      {
        throw new Exception($"Message State Data not found [{dataKey}]");
      }

      var stateData = messageData[dataKey];
      messageData.Remove(dataKey);

      return stateData;
    }
  }
}

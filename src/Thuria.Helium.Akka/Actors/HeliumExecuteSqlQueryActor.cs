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
      var retrieveConnectionStringMessage = new HeliumGetConnectionStringMessage(sqlQueryMessage.DatabaseContextName, Sender, sqlQueryMessage);
      _connectionStringActor.Tell(retrieveConnectionStringMessage);
    }

    private void HandleConnectionStringResult(HeliumGetConnectionStringResultMessage resultMessage)
    {
      var sqlQueryMessage = (HeliumExecuteSqlQueryMessage)resultMessage.OriginalMessage;

      try
      { 
        switch (sqlQueryMessage.HeliumAction)
        {
          case HeliumAction.Retrieve:
            ExecuteSelectSqlQuery(resultMessage.ConnectionString, sqlQueryMessage);
            break;

          default:
            throw new Exception($"Helium Action [{sqlQueryMessage} not currently supported]");
        }
      }
      catch (Exception runtimeException)
      {
        SendActorResultMessage(sqlQueryMessage.HeliumAction, HeliumActionResult.Error, sqlQueryMessage, errorDetail: runtimeException);
      }
    }

    private void ExecuteSelectSqlQuery(string connectionString, HeliumExecuteSqlQueryMessage executeSqlQueryMessage)
    {
      using (var databaseContext = _databaseBuilder.WithDatabaseProviderType(DatabaseProviderType.SqlServer)
                                                   .WithConnectionString(connectionString)
                                                   .BuildReadonly())
      {
        var resultData = databaseContext.Select<object>(executeSqlQueryMessage.SqlQuery);
        SendActorResultMessage(HeliumAction.Retrieve, HeliumActionResult.Success, executeSqlQueryMessage, resultData);
      }
    }

    private void SendActorResultMessage(HeliumAction heliumAction, HeliumActionResult heliumActionResult,
                                        HeliumExecuteSqlQueryMessage executeSqlQueryMessage, IEnumerable<object> resultData = null, object errorDetail = null)
    {
      var resultMessage  = new HeliumExecuteSqlQueryResultMessage(heliumAction, heliumActionResult, executeSqlQueryMessage.OriginalSender, 
                                                                  executeSqlQueryMessage.OriginalMessage, resultData, errorDetail);
      executeSqlQueryMessage.OriginalSender.Tell(resultMessage);
    }
  }
}

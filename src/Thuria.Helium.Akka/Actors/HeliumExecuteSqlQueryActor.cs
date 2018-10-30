using System;

using Akka.Actor;
using Akka.DI.Core;

using Thuria.Helium.Akka.Core;
using Thuria.Helium.Akka.Messages;

namespace Thuria.Helium.Akka.Actors
{
  /// <summary>
  /// Helium Execute SQL Query ACtor
  /// </summary>
  public class HeliumExecuteSqlQueryActor : ReceiveActor
  {
    private readonly IActorRef _connectionStringActor;

    /// <summary>
    /// Helium Execute SQL Query Actor constructor
    /// </summary>
    public HeliumExecuteSqlQueryActor()
    {
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
      var sqlQueryMessage = (HeliumExecuteSqlQueryMessage) resultMessage.OriginalMessage;

      try
      { 
        switch (sqlQueryMessage.HeliumAction)
        {
          case HeliumAction.Retrieve:
            ExecuteSelectSqlQuery(resultMessage.ConnectionString, sqlQueryMessage);
            break;

          default:
            throw new Exception($"Helium Action [{sqlQueryMessage.HeliumAction} not currently supported]");
        }
      }
      catch (Exception runtimeException)
      {
        var queryResultMessage = new HeliumExecuteSqlQueryResultMessage(sqlQueryMessage.Id, sqlQueryMessage.HeliumAction, HeliumActionResult.Error, errorDetail: runtimeException);
        resultMessage.OriginalSender.Tell(queryResultMessage);
      }
    }

    private void ExecuteSelectSqlQuery(string connectionString, HeliumExecuteSqlQueryMessage sqlQueryMessage)
    {
//      using (var databaseContext = this.databaseBuilder.WithDatabaseProviderType(DatabaseProviderType.SqlServer)
//        .WithConnectionString(connectionString)
//        .BuildReadonly())
//      {
//        var resultData = databaseContext.Select<object>(this.sqlQuery);
//
//        var resultMessage = new ExecuteSqlQueryResultMessage(this.originalMessage.ActorMessageId, ActionResult.Success, resultData);
//        this.originalSender.Tell(resultMessage);
//      }
    }
  }
}

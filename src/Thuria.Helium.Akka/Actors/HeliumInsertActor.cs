using Akka.Actor;
using Akka.DI.Core;

using Thuria.Helium.Core;
using Thuria.Helium.Akka.Messages;
using Thuria.Helium.Akka.Core.Messages;

namespace Thuria.Helium.Akka.Actors
{
  /// <summary>
  /// Retrieve Helium Action Actor
  /// </summary>
  public class HeliumInsertActor : HeliumActionActorBase
  {
    private readonly IActorRef _constructInsertSqlQueryActor;
    private readonly IActorRef _executeSqlQueryActor;

    /// <summary>
    /// Helium Insert Actor Constructor
    /// </summary>
    public HeliumInsertActor() 
      : base(HeliumAction.Insert)
    {
      _constructInsertSqlQueryActor = Context.ActorOf(Context.DI().Props<HeliumConstructInsertSqlQueryActor>(), $"HeliumConstructInsertSqlQuery_{HeliumAction.Insert}");
      _executeSqlQueryActor         = Context.ActorOf(Context.DI().Props<HeliumExecuteSqlQueryActor>(), $"HeliumExecuteSqlQuery_{HeliumAction.Insert}");
    }

    /// <inheritdoc />
    protected override void ReadyToPerformAction()
    {
      base.ReadyToPerformAction();

      Receive<HeliumActionMessage>(StartHeliumActionProcessing, message => message.HeliumAction == HeliumAction.Insert);
      Receive<HeliumConstructSqlQueryResultMessage>(HandleSqlQueryResult, message => message.HeliumAction == HeliumAction.Insert);
      Receive<HeliumExecuteSqlQueryResultMessage>(HandleExecuteSqlQueryResult, message => message.HeliumAction == HeliumAction.Insert);
    }

    /// <inheritdoc />
    protected override void StartHeliumActionProcessing(HeliumActionMessage actionMessage)
    {
      var sqlQueryMessage = new HeliumConstructSqlQueryMessage(actionMessage.HeliumAction, actionMessage.DataModel);
      sqlQueryMessage.AddStateData("OriginalInsertSender", Sender);
      sqlQueryMessage.AddStateData("OriginalInsertMessage", actionMessage);

      _constructInsertSqlQueryActor.Tell(sqlQueryMessage);
    }

    private void HandleSqlQueryResult(HeliumConstructSqlQueryResultMessage sqlQueryResultMessage)
    {
      var originalMessage        = (HeliumActionMessage)sqlQueryResultMessage.MessageStateData["OriginalInsertMessage"];
      var executeSqlQueryMessage = new HeliumExecuteSqlQueryMessage(originalMessage.DatabaseContextName, originalMessage.HeliumAction, sqlQueryResultMessage.SqlQuery);
      executeSqlQueryMessage.AddStateData(sqlQueryResultMessage.MessageStateData);

      _executeSqlQueryActor.Tell(executeSqlQueryMessage);
    }

    private void HandleExecuteSqlQueryResult(HeliumExecuteSqlQueryResultMessage executeSqlQueryResultMessage)
    {
      var originalSender      = (IActorRef)executeSqlQueryResultMessage.MessageStateData["OriginalInsertSender"];
      var actionResultMessage = new HeliumActionResultMessage(HeliumActionResult.Success, executeSqlQueryResultMessage.ResultData, 
                                                              executeSqlQueryResultMessage.ErrorDetail?.ToString());

      originalSender.Tell(actionResultMessage);
    }
  }
}

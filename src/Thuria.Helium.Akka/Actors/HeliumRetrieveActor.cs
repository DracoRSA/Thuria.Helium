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
  public class HeliumRetrieveActor : HeliumActionActorBase
  {
    private readonly IActorRef _constructSelectSqlQueryActor;
    private readonly IActorRef _executeSqlQueryActor;

    /// <summary>
    /// Helium Retrieve Actor Constructor
    /// </summary>
    public HeliumRetrieveActor() 
      : base(HeliumAction.Retrieve)
    {
      _constructSelectSqlQueryActor = Context.ActorOf(Context.DI().Props<HeliumConstructSelectSqlQueryActor>(), $"HeliumConstructSelectSqlQuery_{HeliumAction.Retrieve}");
      _executeSqlQueryActor         = Context.ActorOf(Context.DI().Props<HeliumExecuteSqlQueryActor>(), $"HeliumExecuteSqlQuery_{HeliumAction.Retrieve}");
    }

    /// <inheritdoc />
    protected override void ReadyToPerformAction()
    {
      base.ReadyToPerformAction();

      Receive<HeliumActionMessage>(StartHeliumActionProcessing, message => message.HeliumAction == HeliumAction.Retrieve);
      Receive<HeliumConstructSqlQueryResultMessage>(HandleSqlQueryResult, message => message.HeliumAction == HeliumAction.Retrieve);
      Receive<HeliumExecuteSqlQueryResultMessage>(HandleExecuteSqlQueryResult, message => message.HeliumAction == HeliumAction.Retrieve);
    }

    /// <inheritdoc />
    protected override void StartHeliumActionProcessing(HeliumActionMessage actionMessage)
    {
      var sqlQueryMessage = new HeliumConstructSqlQueryMessage(actionMessage.HeliumAction, actionMessage.DataModel);
      sqlQueryMessage.AddStateData("OriginalRetrieveSender", Sender);
      sqlQueryMessage.AddStateData("OriginalRetrieveMessage", actionMessage);

      _constructSelectSqlQueryActor.Tell(sqlQueryMessage);
    }

    private void HandleSqlQueryResult(HeliumConstructSqlQueryResultMessage sqlQueryResultMessage)
    {
      var originalMessage = (HeliumActionMessage)sqlQueryResultMessage.MessageStateData["OriginalRetrieveMessage"];

      var executeSqlQueryMessage = new HeliumExecuteSqlQueryMessage(originalMessage.DatabaseContextName, originalMessage.HeliumAction, sqlQueryResultMessage.SqlQuery);
      executeSqlQueryMessage.AddStateData(sqlQueryResultMessage.MessageStateData);

      _executeSqlQueryActor.Tell(executeSqlQueryMessage);
    }

    private void HandleExecuteSqlQueryResult(HeliumExecuteSqlQueryResultMessage executeSqlQueryResultMessage)
    {
      var originalSender      = (IActorRef)executeSqlQueryResultMessage.MessageStateData["OriginalRetrieveSender"];
      var actionResultMessage = new HeliumActionResultMessage(HeliumActionResult.Success, executeSqlQueryResultMessage.ResultData, 
                                                              executeSqlQueryResultMessage.ErrorDetail?.ToString());

      originalSender.Tell(actionResultMessage);
    }
  }
}

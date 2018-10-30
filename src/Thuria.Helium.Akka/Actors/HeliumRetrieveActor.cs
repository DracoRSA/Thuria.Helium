using Akka.Actor;
using Akka.DI.Core;

using Thuria.Helium.Akka.Core;
using Thuria.Helium.Akka.Messages;

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

      Receive<HeliumConstructSqlQueryResultMessage>(HandleSqlQueryResult, message => message.HeliumAction == HeliumAction.Retrieve);
    }

    /// <inheritdoc />
    protected override void StartHeliumActionProcessing(HeliumActionMessage actionMessage)
    {
      var sqlQueryMessage = new HeliumConstructSqlQueryMessage(actionMessage.HeliumAction, Sender, actionMessage);
      _constructSelectSqlQueryActor.Tell(sqlQueryMessage);
    }

    private void HandleSqlQueryResult(HeliumConstructSqlQueryResultMessage sqlQueryResultMessage)
    {
      var originalMessage        = (HeliumActionMessage)sqlQueryResultMessage.OriginalMessage;
      var executeSqlQueryMessage = new HeliumExecuteSqlQueryMessage(originalMessage.DatabaseContextName, HeliumAction.Retrieve, sqlQueryResultMessage.SqlQuery, 
                                                                    sqlQueryResultMessage.OriginalSender, sqlQueryResultMessage.OriginalMessage);
      _executeSqlQueryActor.Tell(executeSqlQueryMessage);
    }
  }
}

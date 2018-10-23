using Akka.Actor;

namespace Thuria.Helium.Akka.Actors
{
  /// <summary>
  /// Helium Execute SQL Query ACtor
  /// </summary>
  public class HeliumExecuteSqlQueryActor : ReceiveActor
  {
    public HeliumExecuteSqlQueryActor()
    {
      // this.Receive<ExecuteSqlQueryMessage>(message => this.HandleExecuteSqlQuery(message));
    }
  }
}

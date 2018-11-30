using Akka.Actor;

using NUnit.Framework;
using FluentAssertions;
using Akka.TestKit.NUnit3;

using Thuria.Helium.Akka.Actors;

namespace Thuria.Helium.Akka.Tests.Actors
{
  [TestFixture]
  public class TestHeliumRetrieveActor : TestKit
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var actorProps = Props.Create<HeliumRetrieveActor>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var retrieveActor = Sys.ActorOf(actorProps, "Retrieve");
      //---------------Test Result -----------------------
      ExpectNoMsg();
      retrieveActor.Should().NotBeNull();
    }
  }
}

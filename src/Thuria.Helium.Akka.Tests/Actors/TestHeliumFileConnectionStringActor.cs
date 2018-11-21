using Akka.Actor;
using Akka.TestKit.NUnit3;

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Zitidar.Core;
using Thuria.Helium.Akka.Actors;

namespace Thuria.Helium.Akka.Tests.Actors
{
  [TestFixture]
  public class TestHeliumFileConnectionStringActor : TestKit
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var databaseSettings = Substitute.For<IThuriaDatabaseSettings>();
      var actorProps       = Props.Create<HeliumFileConnectionStringActor>(databaseSettings);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var connectionStringActor = Sys.ActorOf(actorProps, "Test");
      //---------------Test Result -----------------------
      connectionStringActor.Should().NotBeNull();
    }

    [Test]
    public void Constructor_GivenNullDatabaseSettings_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var actorProps = Props.Create<HeliumFileConnectionStringActor>();

      //---------------Assert Precondition----------------

      //---------------Execute Test ----------------------
      var connectionStringActor = Sys.ActorOf(actorProps, "Test");
      //---------------Test Result -----------------------
    }
  }
}

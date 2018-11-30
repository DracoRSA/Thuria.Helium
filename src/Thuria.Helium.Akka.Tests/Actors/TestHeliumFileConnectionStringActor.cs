using System;
using Akka.Actor;
using Akka.TestKit.NUnit3;

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Zitidar.Core;
using Thuria.Helium.Akka.Actors;
using Thuria.Helium.Akka.Messages;

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
      ExpectNoMsg();
      connectionStringActor.Should().NotBeNull();
    }

    [Test]
    public void Constructor_GivenNullDatabaseSettings_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      IThuriaDatabaseSettings dbSettings = null;
      var actorProps                     = Props.Create<HeliumFileConnectionStringActor>(dbSettings);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      EventFilter.Exception(typeof(ArgumentNullException), contains: "Parameter name: databaseSettings", checkInnerExceptions: true)
                 .Expect(1, () => Sys.ActorOf(actorProps, "Test"));
      //---------------Test Result -----------------------
    }

    [Test]
    public void HeliumConnectionStringMessage_GivenNoContext_ShouldReturnEmptyConnectionString()
    {
      //---------------Set up test pack-------------------
      var databaseSettings = Substitute.For<IThuriaDatabaseSettings>();
      var actorProps       = Props.Create<HeliumFileConnectionStringActor>(databaseSettings);
      var actorRef         = Sys.ActorOf(actorProps, "Test");

      var getConnectionStringMessage = new HeliumGetConnectionStringMessage(null);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      actorRef.Tell(getConnectionStringMessage);
      //---------------Test Result -----------------------
      var connectionString = ExpectMsg<HeliumGetConnectionStringResultMessage>().ConnectionString;
      connectionString.Should().BeNullOrWhiteSpace();
    }

    [Test]
    public void HeliumConnectionStringMessage_GivenContext_ShouldReturnConnectionStringForContext()
    {
      //---------------Set up test pack-------------------
      var dbContextName           = "DbTestContext";
      var contextConnectionString = "TestConnectionString";

      var databaseSettings = Substitute.For<IThuriaDatabaseSettings>();
      databaseSettings.GetConnectionString(dbContextName).Returns(contextConnectionString);

      var actorProps = Props.Create<HeliumFileConnectionStringActor>(databaseSettings);
      var actorRef   = Sys.ActorOf(actorProps, "Test");

      var getConnectionStringMessage = new HeliumGetConnectionStringMessage(dbContextName);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      actorRef.Tell(getConnectionStringMessage);
      //---------------Test Result -----------------------
      var connectionString = ExpectMsg<HeliumGetConnectionStringResultMessage>().ConnectionString;
      connectionString.Should().Be(contextConnectionString);
    }
  }
}

using System;

using Akka.Actor;
using StructureMap;
using Akka.DI.Core;
using Akka.DI.StructureMap;

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;
using Akka.TestKit.NUnit3;

using Thuria.Helium.Core;
using Thuria.Zitidar.Core;
using Thuria.Helium.Akka.Actors;
using Thuria.Helium.Akka.Messages;
using Thuria.Thark.Core.DataAccess;

namespace Thuria.Helium.Akka.Tests.Actors
{
  [TestFixture]
  public class TestHeliumExecuteSqlQueryActor : TestKit
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var databaseBuilder = Substitute.For<IDatabaseBuilder>();
      var actorProps      = Props.Create<HeliumExecuteSqlQueryActor>(databaseBuilder);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var executeSqlQueryActor = Sys.ActorOf(actorProps, "Test");
      //---------------Test Result -----------------------
      ExpectNoMsg();
      executeSqlQueryActor.Should().NotBeNull();
    }

    [Test]
    public void Constructor_GivenNullDatabaseBuilder_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      IDatabaseBuilder databaseBuilder = null;
      var actorProps                   = Props.Create<HeliumExecuteSqlQueryActor>(databaseBuilder);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      EventFilter.Exception(typeof(ArgumentNullException), contains: "Parameter name: databaseBuilder", checkInnerExceptions: true)
                 .Expect(1, () => Sys.ActorOf(actorProps, "Test"));
      //---------------Test Result -----------------------
    }

    [Test]
    public void HeliumExecuteSqlQueryMessage_GivenValidMessage_ShouldSendMessageToGetConnectionString()
    {
      //---------------Set up test pack-------------------
      var actorRef               = CreateActor();
      var executeSqlQueryMessage = new HeliumExecuteSqlQueryMessage("TestDbContext", HeliumAction.Retrieve, "SELECT * FROM [HeliumFake]");
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      actorRef.Tell(executeSqlQueryMessage);
      //---------------Test Result -----------------------
      ExpectMsg<HeliumExecuteSqlQueryResultMessage>();
    }

    private IActorRef CreateActor(IDatabaseBuilder databaseBuilder = null)
    {
      var databaseSettings = Substitute.For<IThuriaDatabaseSettings>();
      databaseSettings.GetConnectionString("TestDbContext").Returns("TestConnectionString");

      var container = new Container(
        expression =>
          {
            expression.For<IThuriaDatabaseSettings>().Use(databaseSettings);
            expression.For<IDatabaseBuilder>().Use(databaseBuilder ?? Substitute.For<IDatabaseBuilder>());
          });
      var dependencyResolver = new StructureMapDependencyResolver(container, Sys);

      var actorProps = Sys.DI().Props<HeliumExecuteSqlQueryActor>();
      return Sys.ActorOf(actorProps, "Test");
    }
  }
}

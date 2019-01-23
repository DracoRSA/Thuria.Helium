using System;
using Akka.Actor;

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;
using Akka.TestKit.NUnit3;
using Thuria.Calot.TestUtilities;
using Thuria.Helium.Akka.Actors;
using Thuria.Helium.Akka.Messages;
using Thuria.Helium.Core;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.StatementBuilder.Builders;

namespace Thuria.Helium.Akka.Tests.Actors
{
  [TestFixture]
  public class TestHeliumConstructInsertSqlQueryActor : TestKit
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var insertStatementBuilder = Substitute.For<IInsertStatementBuilder>();
      var conditionBuilder       = Substitute.For<IConditionBuilder>();
      var actorProps             = Props.Create<HeliumConstructInsertSqlQueryActor>(insertStatementBuilder, conditionBuilder);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectSqlQueryActor = Sys.ActorOf(actorProps, "Test");
      //---------------Test Result -----------------------
      ExpectNoMsg();
      selectSqlQueryActor.Should().NotBeNull();
    }

    [Test]
    public void Constructor_GivenNullInsertStatementBuilder_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      IInsertStatementBuilder insertStatementBuilder = null;
      var conditionBuilder                           = Substitute.For<IConditionBuilder>();
      var actorProps                                 = Props.Create<HeliumConstructInsertSqlQueryActor>(insertStatementBuilder, conditionBuilder);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      EventFilter.Exception(typeof(ArgumentNullException), contains: "Parameter name: insertStatementBuilder", checkInnerExceptions: true)
                 .Expect(1, () => Sys.ActorOf(actorProps, "Test"));
      //---------------Test Result -----------------------
    }

    [Test]
    public void Constructor_GivenNullConditionBuilder_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var insertStatementBuilder         = Substitute.For<IInsertStatementBuilder>();
      IConditionBuilder conditionBuilder = null;
      var actorProps                     = Props.Create<HeliumConstructInsertSqlQueryActor>(insertStatementBuilder, conditionBuilder);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      EventFilter.Exception(typeof(ArgumentNullException), contains: "Parameter name: conditionBuilder", checkInnerExceptions: true)
                 .Expect(1, () => Sys.ActorOf(actorProps, "Test"));
      //---------------Test Result -----------------------
    }

    [Test]
    public void HeliumConstructSqlQueryMessage_GivenHeliumActionNotInsert_ShouldNotHandleMessage()
    {
      //---------------Set up test pack-------------------
      var actorRef        = CreateActor();
      var sqlQueryMessage = new HeliumConstructSqlQueryMessage(HeliumAction.Retrieve, new HeliumFakeDataModel { Id = Guid.NewGuid() });
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      actorRef.Tell(sqlQueryMessage);
      //---------------Test Result -----------------------
      ExpectNoMsg();
    }

    [Test]
    public void HeliumConstructSqlQueryMessage_GivenValidMessage_ShouldHandleMessage()
    {
      //---------------Set up test pack-------------------
      var actorRef        = CreateActor();
      var heliumFakeDataModel = new HeliumFakeDataModel
        {
          Id          = Guid.NewGuid(),
          Description = RandomValueGenerator.CreateRandomString(1, 10),
          IsActive    = RandomValueGenerator.CreateRandomBoolean()
        };
      var sqlQueryMessage = new HeliumConstructSqlQueryMessage(HeliumAction.Insert, heliumFakeDataModel);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      actorRef.Tell(sqlQueryMessage);
      //---------------Test Result -----------------------
      var resultMessage = ExpectMsg<HeliumConstructSqlQueryResultMessage>();
      resultMessage.Should().NotBeNull();
    }
    
    [Test]
    public void HeliumConstructSqlQueryMessage_GivenValidMessage_ShouldHandleMessageAndReturnExpectedSqlQuery()
    {
      //---------------Set up test pack-------------------
      var heliumFakeDataModel = new HeliumFakeDataModel
        {
          Id          = Guid.NewGuid(),
          Description = RandomValueGenerator.CreateRandomString(1, 10)
        };
      var expectedSqlQuery = $"INSERT INTO [HeliumFake] ([Id],[Description]) VALUES ('{heliumFakeDataModel.Id}','{heliumFakeDataModel.Description}')";
      var actorRef         = CreateActor(InsertStatementBuilder.Create, ConditionBuilder.Create);
      var sqlQueryMessage  = new HeliumConstructSqlQueryMessage(HeliumAction.Insert, heliumFakeDataModel);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      actorRef.Tell(sqlQueryMessage);
      //---------------Test Result -----------------------
      var resultMessage = ExpectMsg<HeliumConstructSqlQueryResultMessage>();
      resultMessage.Should().NotBeNull();
      resultMessage.HeliumAction.Should().Be(sqlQueryMessage.HeliumAction);
      resultMessage.SqlQuery.Should().Be(expectedSqlQuery);
    }

    private IActorRef CreateActor(IInsertStatementBuilder statementBuilder = null, IConditionBuilder conditionBuilder = null)
    {
      var insertStatementBuilder = statementBuilder ?? Substitute.For<IInsertStatementBuilder>();
      var selectConditionBuilder = conditionBuilder ?? Substitute.For<IConditionBuilder>();

      var actorProps = Props.Create<HeliumConstructInsertSqlQueryActor>(insertStatementBuilder, selectConditionBuilder);
      return Sys.ActorOf(actorProps, "Test");
    }
  }
}

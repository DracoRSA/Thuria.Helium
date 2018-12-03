using System;
using Akka.Actor;

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;
using Akka.TestKit.NUnit3;

using Thuria.Helium.Akka.Actors;
using Thuria.Helium.Akka.Messages;
using Thuria.Helium.Core;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.StatementBuilder.Builders;

namespace Thuria.Helium.Akka.Tests.Actors
{
  [TestFixture]
  public class TestHeliumConstructSelectSqlQueryActor : TestKit
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var selectStatementBuilder = Substitute.For<ISelectStatementBuilder>();
      var conditionBuilder       = Substitute.For<IConditionBuilder>();
      var actorProps             = Props.Create<HeliumConstructSelectSqlQueryActor>(selectStatementBuilder, conditionBuilder);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var selectSqlQueryActor = Sys.ActorOf(actorProps, "Test");
      //---------------Test Result -----------------------
      ExpectNoMsg();
      selectSqlQueryActor.Should().NotBeNull();
    }

    [Test]
    public void Constructor_GivenNullSelectStatementBuilder_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      ISelectStatementBuilder selectStatementBuilder = null;
      var conditionBuilder                           = Substitute.For<IConditionBuilder>();
      var actorProps                                 = Props.Create<HeliumConstructSelectSqlQueryActor>(selectStatementBuilder, conditionBuilder);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      EventFilter.Exception(typeof(ArgumentNullException), contains: "Parameter name: selectStatementBuilder", checkInnerExceptions: true)
                 .Expect(1, () => Sys.ActorOf(actorProps, "Test"));
      //---------------Test Result -----------------------
    }

    [Test]
    public void Constructor_GivenNullConditionBuilder_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var selectStatementBuilder         = Substitute.For<ISelectStatementBuilder>();
      IConditionBuilder conditionBuilder = null;
      var actorProps                     = Props.Create<HeliumConstructSelectSqlQueryActor>(selectStatementBuilder, conditionBuilder);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      EventFilter.Exception(typeof(ArgumentNullException), contains: "Parameter name: conditionBuilder", checkInnerExceptions: true)
                 .Expect(1, () => Sys.ActorOf(actorProps, "Test"));
      //---------------Test Result -----------------------
    }

    [Test]
    public void HeliumConstructSqlQueryMessage_GivenHeliumActionNotRetrieve_ShouldNotHandleMessage()
    {
      //---------------Set up test pack-------------------
      var actorRef        = CreateActor();
      var sqlQueryMessage = new HeliumConstructSqlQueryMessage(HeliumAction.Insert, new HeliumFakeDataModel { Id = Guid.NewGuid() });
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
      var sqlQueryMessage = new HeliumConstructSqlQueryMessage(HeliumAction.Retrieve, new HeliumFakeDataModel { Id = Guid.NewGuid() });
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
      var dataModelId      = Guid.NewGuid();
      var expectedSqlQuery = $"SELECT [HeliumFake].[Id], [HeliumFake].[Description], [HeliumFake].[IsActive] FROM [HeliumFake] WHERE  [HeliumFake].[Id] = '{dataModelId}' ";
      var actorRef         = CreateActor(SelectStatementBuilder.Create, ConditionBuilder.Create);
      var sqlQueryMessage  = new HeliumConstructSqlQueryMessage(HeliumAction.Retrieve, new HeliumFakeDataModel { Id = dataModelId });
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      actorRef.Tell(sqlQueryMessage);
      //---------------Test Result -----------------------
      var resultMessage = ExpectMsg<HeliumConstructSqlQueryResultMessage>();
      resultMessage.Should().NotBeNull();
      resultMessage.HeliumAction.Should().Be(sqlQueryMessage.HeliumAction);
      resultMessage.SqlQuery.Should().Be(expectedSqlQuery);
    }

    private IActorRef CreateActor(ISelectStatementBuilder statementBuilder = null, IConditionBuilder conditionBuilder = null)
    {
      var selectStatementBuilder = statementBuilder ?? Substitute.For<ISelectStatementBuilder>();
      var selectConditionBuilder = conditionBuilder ?? Substitute.For<IConditionBuilder>();

      var actorProps = Props.Create<HeliumConstructSelectSqlQueryActor>(selectStatementBuilder, conditionBuilder);
      return Sys.ActorOf(actorProps, "Test");
    }
  }
}

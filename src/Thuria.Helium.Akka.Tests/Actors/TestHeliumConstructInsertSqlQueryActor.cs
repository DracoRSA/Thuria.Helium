using System;
using Akka.Actor;

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;
using Akka.TestKit.NUnit3;

using Thuria.Helium.Akka.Actors;
using Thuria.Thark.Core.Statement.Builders;

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
  }
}

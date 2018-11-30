using System;
using Akka.Actor;
using Akka.TestKit.NUnit3;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Thuria.Helium.Akka.Actors;
using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.Core.Statement.Builders;

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
  }
}

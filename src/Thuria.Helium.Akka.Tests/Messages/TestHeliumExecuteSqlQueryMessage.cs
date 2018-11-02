using Akka.Actor;

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Helium.Akka.Core;
using Thuria.Helium.Akka.Messages;

namespace Thuria.Helium.Akka.Tests.Messages
{
  [TestFixture]
  public class TestHeliumExecuteSqlQueryMessage
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var originalSender = Substitute.For<IActorRef>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var executeSqlQueryMessage = new HeliumExecuteSqlQueryMessage("dbContext", HeliumAction.Retrieve, "SELECT * FROM [Test]", originalSender, null);
      //---------------Test Result -----------------------
      executeSqlQueryMessage.Should().NotBeNull();
    }
  }
}

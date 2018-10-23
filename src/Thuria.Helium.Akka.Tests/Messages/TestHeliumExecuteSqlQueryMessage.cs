using System;

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
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var executeSqlQueryMessage = new HeliumExecuteSqlQueryMessage(Guid.NewGuid(), HeliumAction.Retrieve, "SELECT * FROM [Test]");
      //---------------Test Result -----------------------
      executeSqlQueryMessage.Should().NotBeNull();
    }
  }
}

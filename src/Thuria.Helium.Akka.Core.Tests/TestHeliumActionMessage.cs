using NUnit.Framework;
using FluentAssertions;

using Thuria.Helium.Core;
using Thuria.Helium.Akka.Core.Messages;

namespace Thuria.Helium.Akka.Core.Tests
{
  [TestFixture]
  public class TestHeliumActionMessage
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var actionMessage = new HeliumActionMessage(HeliumAction.None, null);
      //---------------Test Result -----------------------
      actionMessage.Should().NotBeNull();
    }
  }
}

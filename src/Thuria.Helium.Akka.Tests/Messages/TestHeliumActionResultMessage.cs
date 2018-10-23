using NUnit.Framework;
using FluentAssertions;

using Thuria.Helium.Akka.Core;
using Thuria.Calot.TestUtilities;
using Thuria.Helium.Akka.Messages;

namespace Thuria.Helium.Akka.Tests.Messages
{
  [TestFixture]
  public class TestHeliumActionResultMessage
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var actionMessage = new HeliumActionResultMessage(HeliumActionResult.Unknown);
      //---------------Test Result -----------------------
      actionMessage.Should().NotBeNull();
    }

    [TestCase("heliumActionResult", "HeliumActionResult")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<HeliumActionResultMessage>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}

using NUnit.Framework;
using FluentAssertions;

using Thuria.Helium.Core;
using Thuria.Calot.TestUtilities;
using Thuria.Helium.Akka.Core.Messages;

namespace Thuria.Helium.Akka.Core.Tests
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
      var resultMessage = new HeliumActionResultMessage(HeliumActionResult.Unknown);
      //---------------Test Result -----------------------
      resultMessage.Should().NotBeNull();
    }

    [TestCase("heliumActionResult", "HeliumActionResult")]
    [TestCase("resultData", "ResultData")]
    [TestCase("errorDetail", "ErrorDetail")]
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

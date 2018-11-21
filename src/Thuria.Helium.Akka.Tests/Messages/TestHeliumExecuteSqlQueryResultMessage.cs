using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;
using Thuria.Helium.Akka.Messages;
using Thuria.Helium.Core;

namespace Thuria.Helium.Akka.Tests.Messages
{
  [TestFixture]
  public class TestHeliumExecuteSqlQueryResultMessage
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var executeSqlQueryMessage = new HeliumExecuteSqlQueryResultMessage(HeliumAction.Insert, HeliumActionResult.Error);
      //---------------Test Result -----------------------
      executeSqlQueryMessage.Should().NotBeNull();
    }

    [TestCase("heliumAction", "HeliumAction")]
    [TestCase("actionResult", "ActionResult")]
    [TestCase("resultData", "ResultData")]
    [TestCase("errorDetail", "ErrorDetail")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<HeliumExecuteSqlQueryResultMessage>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}

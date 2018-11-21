using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;
using Thuria.Helium.Akka.Messages;

namespace Thuria.Helium.Akka.Tests.Messages
{
  [TestFixture]
  public class TestHeliumGetConnectionStringResultMessage
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var resultMessage = new HeliumGetConnectionStringResultMessage("ConnectionString");
      //---------------Test Result -----------------------
      resultMessage.Should().NotBeNull();
    }

    [TestCase("connectionString", "ConnectionString")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<HeliumGetConnectionStringResultMessage>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}

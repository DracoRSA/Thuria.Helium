using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;
using Thuria.Helium.Akka.Messages;

namespace Thuria.Helium.Akka.Tests.Messages
{
  [TestFixture]
  public class TestHeliumGetConnectionStringMessage
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var getConnectionStringMessage = new HeliumGetConnectionStringMessage("Test");
      //---------------Test Result -----------------------
      getConnectionStringMessage.Should().NotBeNull();
    }

    [TestCase("dbContextName", "DbContextName")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<HeliumGetConnectionStringMessage>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}

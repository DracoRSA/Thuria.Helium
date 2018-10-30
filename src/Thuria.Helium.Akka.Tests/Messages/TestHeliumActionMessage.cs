using NUnit.Framework;
using FluentAssertions;

using Thuria.Helium.Akka.Core;
using Thuria.Calot.TestUtilities;
using Thuria.Helium.Akka.Messages;

namespace Thuria.Helium.Akka.Tests.Messages
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
      var actionMessage = new HeliumActionMessage(HeliumAction.None, new object());
      //---------------Test Result -----------------------
      actionMessage.Should().NotBeNull();
    }

    [TestCase("heliumAction", "HeliumAction")]
    [TestCase("dataModel", "DataModel")]
    [TestCase("dbContextName", "DatabaseContextName")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<HeliumActionMessage>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}

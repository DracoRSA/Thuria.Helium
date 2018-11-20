using NUnit.Framework;
using FluentAssertions;

using Thuria.Helium.Core;
using Thuria.Calot.TestUtilities;
using Thuria.Helium.Akka.Messages;

namespace Thuria.Helium.Akka.Tests.Messages
{
  [TestFixture]
  public class TestHeliumConstructSqlQueryResultMessage
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var actionMessage = new HeliumConstructSqlQueryResultMessage(HeliumAction.Retrieve, "SELECT * FROM [Test]");
      //---------------Test Result -----------------------
      actionMessage.Should().NotBeNull();
    }

    [TestCase("heliumAction", "HeliumAction")]
    [TestCase("sqlQuery", "SqlQuery")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<HeliumConstructSqlQueryResultMessage>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}

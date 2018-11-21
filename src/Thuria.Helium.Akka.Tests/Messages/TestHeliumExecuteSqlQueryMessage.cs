using NUnit.Framework;
using FluentAssertions;

using Thuria.Helium.Core;
using Thuria.Calot.TestUtilities;
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
      var executeSqlQueryMessage = new HeliumExecuteSqlQueryMessage("dbContext", HeliumAction.Retrieve, "SELECT * FROM [Test]");
      //---------------Test Result -----------------------
      executeSqlQueryMessage.Should().NotBeNull();
    }

    [TestCase("dbContextName")]
    [TestCase("sqlQuery")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<HeliumExecuteSqlQueryMessage>(parameterName);
      //---------------Test Result -----------------------
    }

    [TestCase("dbContextName", "DatabaseContextName")]
    [TestCase("heliumAction", "HeliumAction")]
    [TestCase("sqlQuery", "SqlQuery")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<HeliumExecuteSqlQueryMessage>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}

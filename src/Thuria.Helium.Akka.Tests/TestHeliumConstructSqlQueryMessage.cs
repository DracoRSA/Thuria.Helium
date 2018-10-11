using System;
using FluentAssertions;
using NUnit.Framework;
using Thuria.Calot.TestUtilities;
using Thuria.Helium.Akka.Core;
using Thuria.Helium.Akka.Messages;

namespace Thuria.Helium.Akka.Tests
{
  [TestFixture]
  public class TestHeliumConstructSqlQueryMessage
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var actionMessage = new HeliumConstructSqlQueryMessage(Guid.NewGuid(), HeliumAction.None, new object());
      //---------------Test Result -----------------------
      actionMessage.Should().NotBeNull();
    }

    [TestCase("messageId")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<HeliumConstructSqlQueryMessage>(parameterName);
      //---------------Test Result -----------------------
    }

    [TestCase("messageId", "Id")]
    [TestCase("heliumAction", "HeliumAction")]
    [TestCase("dataModel", "DataModel")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidatePropertySetWithParameter<HeliumConstructSqlQueryMessage>(parameterName, propertyName);
      //---------------Test Result -----------------------
    }
  }
}

using System;

using NUnit.Framework;
using FluentAssertions;

using Thuria.Helium.Akka.Core;
using Thuria.Calot.TestUtilities;
using Thuria.Helium.Akka.Messages;

namespace Thuria.Helium.Akka.Tests
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
      var actionMessage = new HeliumConstructSqlQueryResultMessage(Guid.NewGuid(), HeliumAction.None, new object(), String.Empty);
      //---------------Test Result -----------------------
      actionMessage.Should().NotBeNull();
    }

    [TestCase("messageId", "Id")]
    [TestCase("heliumAction", "HeliumAction")]
    [TestCase("dataModel", "DataModel")]
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

using Akka.Actor;

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Helium.Core;
using Thuria.Helium.Akka.Core;
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
      var originalSender = Substitute.For<IActorRef>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var actionMessage = new HeliumConstructSqlQueryResultMessage(HeliumAction.Retrieve, "SELECT * FROM [Test]", originalSender, null);
      //---------------Test Result -----------------------
      actionMessage.Should().NotBeNull();
    }

    [TestCase("heliumAction", "HeliumAction")]
    [TestCase("sqlQuery", "SqlQuery")]
    [TestCase("originalSender", "OriginalSender")]
    [TestCase("originalMessage", "OriginalMessage")]
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

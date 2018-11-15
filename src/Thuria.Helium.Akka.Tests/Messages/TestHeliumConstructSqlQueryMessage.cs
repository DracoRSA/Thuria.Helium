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
  public class TestHeliumConstructSqlQueryMessage
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var originalSender = Substitute.For<IActorRef>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var actionMessage = new HeliumConstructSqlQueryMessage(HeliumAction.None, originalSender, null);
      //---------------Test Result -----------------------
      actionMessage.Should().NotBeNull();
    }

    [TestCase("originalSender")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<HeliumConstructSqlQueryMessage>(parameterName);
      //---------------Test Result -----------------------
    }

    [TestCase("heliumAction", "HeliumAction")]
    [TestCase("originalSender", "OriginalSender")]
    [TestCase("originalMessage", "OriginalMessage")]
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

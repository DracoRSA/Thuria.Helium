using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Zitidar.Akka;
using Thuria.Zitidar.Core;
using Nancy.Responses.Negotiation;
using Thuria.Calot.TestUtilities;

namespace Thuria.Helium.Akka.Nancy.Tests
{
  [TestFixture]
  public class TestHeliumInsertModule
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var thuriaActorSystem = Substitute.For<IThuriaActorSystem>();
      var thuriaSerializer = Substitute.For<IThuriaSerializer>();
      var responseNegotiator = Substitute.For<IResponseNegotiator>();
      var thuriaLogger = Substitute.For<IThuriaLogger>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var insertModule = new HeliumInsertModule(thuriaActorSystem, thuriaSerializer, responseNegotiator, thuriaLogger);
      //---------------Test Result -----------------------
      insertModule.Should().NotBeNull();
    }

    [TestCase("heliumActorSystem")]
    [TestCase("thuriaSerializer")]
    [TestCase("responseNegotiator")]
    [TestCase("thuriaLogger")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<HeliumInsertModule>(parameterName);
      //---------------Test Result -----------------------
    }
  }
}
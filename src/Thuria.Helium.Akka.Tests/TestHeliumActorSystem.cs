using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Zitidar.Core;
using Thuria.Calot.TestUtilities;

namespace Thuria.Helium.Akka.Tests
{
  [TestFixture]
  public class TestHeliumActorSystem
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var iocContainer = Substitute.For<IThuriaIocContainer>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var actorSystem = new HeliumActorSystem(iocContainer);
      //---------------Test Result -----------------------
      actorSystem.Should().NotBeNull();
    }

    [TestCase("iocContainer")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<HeliumActorSystem>(parameterName);
      //---------------Test Result -----------------------
    }
  }
}

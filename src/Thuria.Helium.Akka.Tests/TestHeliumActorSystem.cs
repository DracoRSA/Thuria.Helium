using NSubstitute;
using NUnit.Framework;
using FluentAssertions;
using StructureMap;
using Thuria.Zitidar.Core;
using Thuria.Calot.TestUtilities;
using Thuria.Zitidar.Structuremap;

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

    [Test]
    public void Name_ShouldBeExpectedName()
    {
      //---------------Set up test pack-------------------
      var iocContainer = Substitute.For<IThuriaIocContainer>();
      var actorSystem  = new HeliumActorSystem(iocContainer);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var actorSystemName = actorSystem.Name;
      //---------------Test Result -----------------------
      actorSystemName.Should().Be("Helium");
    }

    [Test]
    public void Start_ShouldNotThrowExceptionAndCreateActorSystem()
    {
      //---------------Set up test pack-------------------
      var iocContainer = CreateIocContainer();
      using (var actorSystem = new HeliumActorSystem(iocContainer))
      {
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        Assert.DoesNotThrow(() => actorSystem.Start());
        //---------------Test Result -----------------------
        actorSystem.ActorSystem.Should().NotBeNull();
      }
    }

    private IThuriaIocContainer CreateIocContainer()
    {
      var container = new Container(
        expression =>
          {
            expression.For<IThuriaIocContainer>().Use<StructuremapThuriaIocContainer>();
          });

      var iocContainer = container.GetInstance<IThuriaIocContainer>();
      iocContainer.Should().NotBeNull("Thuria IOC Container not registered in the Container");

      return iocContainer;
    }
  }
}

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using StructureMap;

using Thuria.Zitidar.Akka;
using Thuria.Zitidar.Core;
using Nancy.Responses.Negotiation;
using Thuria.Calot.TestUtilities;
using Thuria.Zitidar.Structuremap;

namespace Thuria.Helium.Akka.Nancy.Tests
{
  [TestFixture]
  public class TestHeliumRetrieveModule
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var thuriaActorSystem  = Substitute.For<IThuriaActorSystem>();
      var thuriaSerializer   = Substitute.For<IThuriaSerializer>();
      var responseNegotiator = Substitute.For<IResponseNegotiator>();
      var thuriaLogger       = Substitute.For<IThuriaLogger>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var retrieveModule = new HeliumRetrieveModule(thuriaActorSystem, thuriaSerializer, responseNegotiator, thuriaLogger);
      //---------------Test Result -----------------------
      retrieveModule.Should().NotBeNull();
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
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<HeliumRetrieveModule>(parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void Retrieve_GivenEmptyBody_ShouldReturnBadRequest()
    {
      //---------------Set up test pack-------------------
      
      //---------------Assert Precondition----------------

      //---------------Execute Test ----------------------

      //---------------Test Result -----------------------
      Assert.Fail("Test Not Yet Implemented");
    }

    private IThuriaIocContainer CreateIocContainer()
    {
      var container = new Container(
        expression =>
          {
            expression.For<IThuriaIocContainer>().Use<StructuremapThuriaIocContainer>();
          });

      var iocContainer = container.GetInstance<IThuriaIocContainer>();
      iocContainer.Should().NotBeNull();
      return iocContainer;
    }

    private NancyTestBootstrapper CreateNancyTestBootstrapper(IThuriaIocContainer iocContainer)
    {
      var instanceContainer = iocContainer.GetInstance<IContainer>();
      Assert.IsNotNull(instanceContainer);

      var bootstrapper = new NancyTestBootstrapper(instanceContainer);
      // bootstrapper.AddInstanceToContainer(typeof(IExperianDataManager), experianDataManager);

      return bootstrapper;
    }
  }
}

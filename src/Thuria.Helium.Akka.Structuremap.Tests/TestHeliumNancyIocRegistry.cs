using FluentAssertions;
using Nancy.Responses.Negotiation;
using NSubstitute;
using NUnit.Framework;
using StructureMap;
using Thuria.Helium.Akka.Nancy;
using Thuria.Zitidar.Core;

namespace Thuria.Helium.Akka.Structuremap.Tests
{
  [TestFixture]
  public class TestHeliumNancyIocRegistry
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var iocRegistry = new HeliumNancyIocRegistry();
      //---------------Test Result -----------------------
      iocRegistry.Should().NotBeNull();
    }

    [Test]
    public void Constructor_ShouldRegisterHeliumRetrieveModule()
    {
      //---------------Set up test pack-------------------
      var iocContainer = CreateIocContainer();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var instance = iocContainer.GetInstance<HeliumRetrieveModule>();
      //---------------Test Result -----------------------
      instance.Should().NotBeNull();
      instance.Should().BeOfType<HeliumRetrieveModule>();
    }

    [Test]
    public void Constructor_ShouldRegisterHeliumInsertModule()
    {
      //---------------Set up test pack-------------------
      var iocContainer = CreateIocContainer();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var instance = iocContainer.GetInstance<HeliumInsertModule>();
      //---------------Test Result -----------------------
      instance.Should().NotBeNull();
      instance.Should().BeOfType<HeliumInsertModule>();
    }

    private IContainer CreateIocContainer()
    {
      return new Container(
        expression =>
          {
            expression.AddRegistry<HeliumAkkaIocRegistry>();
            expression.AddRegistry<HeliumNancyIocRegistry>();

            expression.For<IThuriaIocContainer>().Use(Substitute.For<IThuriaIocContainer>());
            expression.For<IThuriaSerializer>().Use(Substitute.For<IThuriaSerializer>());
            expression.For<IResponseNegotiator>().Use(Substitute.For<IResponseNegotiator>());
            expression.For<IThuriaLogger>().Use(Substitute.For<IThuriaLogger>());
          });
    }
  }
}

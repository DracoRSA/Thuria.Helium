using System.Linq;

using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.StructureMap;

using NUnit.Framework;
using FluentAssertions;

using StructureMap;

using Thuria.Zitidar.Akka;
using Thuria.Zitidar.Core;
using Thuria.Helium.Akka.Actors;
using Thuria.Zitidar.Structuremap;
using Thuria.Zitidar.Akka.Settings;

namespace Thuria.Helium.Akka.Structuremap.Tests
{
  [TestFixture]
  public class TestHeliumAkkaIocRegistry
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var iocRegistry = new HeliumAkkaIocRegistry();
      //---------------Test Result -----------------------
      Assert.IsNotNull(iocRegistry);
    }

    [Test]
    public void Constructor_ShouldRegisterIThuriaActorSystem()
    {
      //---------------Set up test pack-------------------
      var iocContainer = CreateIocContainer();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var instance = iocContainer.GetInstance<IThuriaActorSystem>("Helium");
      //---------------Test Result -----------------------
      instance.Should().NotBeNull();
      instance.Should().BeOfType<HeliumActorSystem>();
    }

    [Test]
    public void Constructor_ShouldRegisterIIThuriaStartable()
    {
      //---------------Set up test pack-------------------
      var iocContainer = CreateIocContainer();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var allInstances = iocContainer.GetAllInstances<IThuriaStartable>();
      //---------------Test Result -----------------------
      allInstances.Should().NotBeNull();
      var thuriaStartables = allInstances as IThuriaStartable[] ?? allInstances.ToArray();

      thuriaStartables.Count().Should().Be(1);
      thuriaStartables.First().Should().BeOfType<HeliumActorSystem>();
    }

    [Test]
    public void Constructor_ShouldRegisterIThuriaStoppable()
    {
      //---------------Set up test pack-------------------
      var iocContainer = CreateIocContainer();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var allInstances = iocContainer.GetAllInstances<IThuriaStoppable>();
      //---------------Test Result -----------------------
      allInstances.Should().NotBeNull();
      var thuriaStoppables = allInstances as IThuriaStoppable[] ?? allInstances.ToArray();

      thuriaStoppables.Count().Should().Be(1);
      thuriaStoppables.First().Should().BeOfType<HeliumActorSystem>();
    }

    [Test]
    public void Constructor_ShouldRegisterAllDependenciesForHeliumRetrieveActor()
    {
      //---------------Set up test pack-------------------
      var iocContainer   = CreateIocContainer();
      var actorSystem    = CreateActorSystem(iocContainer);
      IActorRef actorRef = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => actorRef = actorSystem.ActorOf(actorSystem.DI().Props<HeliumRetrieveActor>(), "HeliumRetrieveAction"));
      //---------------Test Result -----------------------
      actorRef.Should().NotBeNull();
    }

    [Test]
    public void Constructor_ShouldRegisterAllDependenciesForHeliumInsertActor()
    {
      //---------------Set up test pack-------------------
      var iocContainer   = CreateIocContainer();
      var actorSystem    = CreateActorSystem(iocContainer);
      IActorRef actorRef = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => actorRef = actorSystem.ActorOf(actorSystem.DI().Props<HeliumInsertActor>(), "HeliumInsertAction"));
      //---------------Test Result -----------------------
      actorRef.Should().NotBeNull();
    }

    [Test]
    public void Constructor_ShouldRegisterAllDependenciesForHeliumConstructSelectSqlQueryActor()
    {
      //---------------Set up test pack-------------------
      var iocContainer   = CreateIocContainer();
      var actorSystem    = CreateActorSystem(iocContainer);
      IActorRef actorRef = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => actorRef = actorSystem.ActorOf(actorSystem.DI().Props<HeliumConstructSelectSqlQueryActor>(), "HeliumConstructSelectSqlQueryActor"));
      //---------------Test Result -----------------------
      actorRef.Should().NotBeNull();
    }

    [Test]
    public void Constructor_ShouldRegisterAllDependenciesForHeliumConstructInsertSqlQueryActor()
    {
      //---------------Set up test pack-------------------
      var iocContainer   = CreateIocContainer();
      var actorSystem    = CreateActorSystem(iocContainer);
      IActorRef actorRef = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => actorRef = actorSystem.ActorOf(actorSystem.DI().Props<HeliumConstructInsertSqlQueryActor>(), "HeliumConstructInsertSqlQueryActor"));
      //---------------Test Result -----------------------
      actorRef.Should().NotBeNull();
    }

    [Test]
    public void Constructor_ShouldRegisterAllDependenciesForHeliumExecuteSqlQueryActor()
    {
      //---------------Set up test pack-------------------
      var iocContainer   = CreateIocContainer();
      var actorSystem    = CreateActorSystem(iocContainer);
      IActorRef actorRef = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => actorRef = actorSystem.ActorOf(actorSystem.DI().Props<HeliumExecuteSqlQueryActor>(), "HeliumExecuteSqlQueryActor"));
      //---------------Test Result -----------------------
      actorRef.Should().NotBeNull();
    }

    [Test]
    public void Constructor_ShouldRegisterAllDependenciesForHeliumFileConnectionStringActor()
    {
      //---------------Set up test pack-------------------
      var iocContainer   = CreateIocContainer();
      var actorSystem    = CreateActorSystem(iocContainer);
      IActorRef actorRef = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => actorRef = actorSystem.ActorOf(actorSystem.DI().Props<HeliumFileConnectionStringActor>(), "HeliumFileConnectionStringActor"));
      //---------------Test Result -----------------------
      actorRef.Should().NotBeNull();
    }

    private IThuriaIocContainer CreateIocContainer()
    {
      var container = new Container(
        expression =>
          {
            expression.AddRegistry<HeliumAkkaIocRegistry>();

            expression.For<IThuriaIocContainer>().Use<StructuremapThuriaIocContainer>();
          });

      var iocContainer = container.GetInstance<IThuriaIocContainer>();
      iocContainer.Should().NotBeNull();

      return iocContainer;
    }

    private ActorSystem CreateActorSystem(IThuriaIocContainer iocContainer)
    {
      var actorSystem                                   = ActorSystem.Create("TestHeliumAkkaIocRegistrySystem", ThuriaHoconLoader.FromFile("akka.config"));
      StructureMapDependencyResolver dependencyResolver = new StructureMapDependencyResolver((IContainer)iocContainer.Container, actorSystem);

      return actorSystem;
    }
  }
}

using NUnit.Framework;
using FluentAssertions;

using StructureMap;

using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataAccess.Builders;
using Thuria.Thark.Core.Statement.Builders;
using Thuria.Thark.StatementBuilder.Builders;

namespace Thuria.Helium.Akka.Structuremap.Tests
{
  [TestFixture]
  public class TestHeliumTharkIocRegistry
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var iocRegistry = new HeliumTharkIocRegistry();
      //---------------Test Result -----------------------
      iocRegistry.Should().NotBeNull();
    }

    [Test]
    public void Constructor_ShouldRegisterIDatabaseBuilder()
    {
      //---------------Set up test pack-------------------
      var iocContainer = CreateIocContainer();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var instance = iocContainer.GetInstance<IDatabaseBuilder>();
      //---------------Test Result -----------------------
      instance.Should().NotBeNull();
      instance.Should().BeOfType<DatabaseBuilder>();
    }

    [Test]
    public void Constructor_ShouldRegisterISelectStatementBuilder()
    {
      //---------------Set up test pack-------------------
      var iocContainer = CreateIocContainer();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var instance = iocContainer.GetInstance<ISelectStatementBuilder>();
      //---------------Test Result -----------------------
      instance.Should().NotBeNull();
      instance.Should().BeOfType<SelectStatementBuilder>();
    }

    [Test]
    public void Constructor_ShouldRegisterIConditionBuilder()
    {
      //---------------Set up test pack-------------------
      var iocContainer = CreateIocContainer();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var instance = iocContainer.GetInstance<IConditionBuilder>();
      //---------------Test Result -----------------------
      instance.Should().NotBeNull();
      instance.Should().BeOfType<ConditionBuilder>();
    }

    private IContainer CreateIocContainer()
    {
      return new Container(
        expression =>
          {
            expression.AddRegistry<HeliumTharkIocRegistry>();
          });
    }
  }
}

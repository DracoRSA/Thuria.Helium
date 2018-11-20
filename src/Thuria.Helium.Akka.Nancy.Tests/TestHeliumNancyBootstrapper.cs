using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using StructureMap;

namespace Thuria.Helium.Akka.Nancy.Tests
{
  [TestFixture]
  public class TestHeliumNancyBootstrapper
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var container = Substitute.For<IContainer>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var nancyBootstrapper = new HeliumNancyBootstrapper(container);
      //---------------Test Result -----------------------
      nancyBootstrapper.Should().NotBeNull();
    }
  }
}

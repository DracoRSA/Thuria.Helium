using NUnit.Framework;

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
  }
}

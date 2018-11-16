using NUnit.Framework;
using FluentAssertions;

namespace Thuria.Helium.Core.Tests
{
  [TestFixture]
  public class TestHeliumRequestModel
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var requestModel = new HeliumRequestModel();
      //---------------Test Result -----------------------
      requestModel.Should().NotBeNull();
    }
  }
}

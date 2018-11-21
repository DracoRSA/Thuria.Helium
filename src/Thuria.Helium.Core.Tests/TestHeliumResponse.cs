using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;

namespace Thuria.Helium.Core.Tests
{
  [TestFixture]
  public class TestHeliumResponse
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var heliumResponse = new HeliumResponse();
      //---------------Test Result -----------------------
      heliumResponse.Should().NotBeNull();
    }

    [TestCase("ActionResult")]
    [TestCase("ResultData")]
    [TestCase("ErrorDetail")]
    public void Properties_GivenValue_ShouldSetPropertyValue(string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      PropertyTestHelper.ValidateGetAndSet<HeliumResponse>(propertyName);
      //---------------Test Result -----------------------
    }
  }
}

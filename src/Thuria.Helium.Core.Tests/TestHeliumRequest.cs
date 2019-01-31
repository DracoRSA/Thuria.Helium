using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;

namespace Thuria.Helium.Core.Tests
{
  [TestFixture]
  public class TestHeliumRequest
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var requestModel = new HeliumRequest();
      //---------------Test Result -----------------------
      requestModel.Should().NotBeNull();
    }

    [TestCase("Action")]
    [TestCase("RequestData")]
    [TestCase("DbContext")]
    public void Properties_GivenValue_ShouldSetPropertyValue(string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      PropertyTestHelper.ValidateGetAndSet<HeliumRequest>(propertyName);
      //---------------Test Result -----------------------
    }
  }
}

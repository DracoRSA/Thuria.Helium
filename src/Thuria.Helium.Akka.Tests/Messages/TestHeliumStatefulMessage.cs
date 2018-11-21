using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;
using Thuria.Helium.Akka.Messages;

namespace Thuria.Helium.Akka.Tests.Messages
{
  [TestFixture]
  public class TestHeliumStatefulMessage
  {
    [Test]
    public void MessageStateData_ShouldCreateConcurrentDictionaryByDefault()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var statefulMessage = new FakeStatefulMessage();
      //---------------Test Result -----------------------
      statefulMessage.MessageStateData.Should().NotBeNull();
      statefulMessage.MessageStateData.Should().BeOfType<ConcurrentDictionary<string, object>>();
    }

    [TestCase("dataKey")]
    [TestCase("stateData")]
    [TestCase("stateDataList")]
    public void AddStateData_GivenNullParameterValue_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      MethodTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<FakeStatefulMessage>("AddStateData", parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void AddStateData_GivenValidData_ShouldAddToStateData()
    {
      //---------------Set up test pack-------------------
      var dataKey         = "dataKey";
      var stateData       = new object();
      var statefulMessage = new FakeStatefulMessage();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      statefulMessage.AddStateData(dataKey, stateData);
      //---------------Test Result -----------------------
      statefulMessage.MessageStateData.Should().ContainKey(dataKey);
      statefulMessage.MessageStateData.Should().ContainValue(stateData);
    }

    [Test]
    public void AddStateData_GivenValidDictionaryData_ShouldAddToStateData()
    {
      //---------------Set up test pack-------------------
      var stateData = new Dictionary<string, object>
        {
          { "dataKey1", new object() },
          { "dataKey2", new object() }
        };
      var statefulMessage = new FakeStatefulMessage();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      statefulMessage.AddStateData(stateData);
      //---------------Test Result -----------------------
      statefulMessage.MessageStateData.Count.Should().Be(stateData.Count);
      statefulMessage.MessageStateData.Should().ContainKey(stateData.Keys.ToArray()[0]);
      statefulMessage.MessageStateData.Should().ContainKey(stateData.Keys.ToArray()[1]);
      statefulMessage.MessageStateData.Should().ContainValue(stateData["dataKey1"]);
      statefulMessage.MessageStateData.Should().ContainValue(stateData["dataKey2"]);
    }

    private class FakeStatefulMessage : HeliumStatefulMessage
    {
    }
  }
}

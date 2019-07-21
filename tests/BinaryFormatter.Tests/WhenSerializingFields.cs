using FluentAssertions;
using Xunit;

namespace BinaryFormatter.Tests
{
    public class WhenSerializingFields
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            // arrange
            var obj = TestHelper.Create<StreamMessage>();

            // act
            var fromBytes = TestHelper.SerializeAndDeserialize(obj);

            // assert
            fromBytes.Should().NotBeNull();
            fromBytes.StreamContent.Should().Be(obj.StreamContent);
            fromBytes.StreamType.Should().Be(obj.StreamType);
            fromBytes.StreamSize.Should().Be(obj.StreamSize);
            fromBytes.StreamContent.Should().Be(obj.StreamContent);
        }

        public class StreamMessage
        {
            public string StreamName;
            public int StreamType;
            public float StreamSize;
            public byte StreamContent;
        }
    }
}

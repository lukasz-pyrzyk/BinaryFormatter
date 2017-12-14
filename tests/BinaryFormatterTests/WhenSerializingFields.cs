using AutoFixture;
using BinaryFormatter;
using FluentAssertions;
using Xunit;

namespace BinaryFormatterTests
{
    public class WhenSerializingFields
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public void CanSerializeAndDeserialize()
        {
            var obj = Fixture.Create<StreamMessage>();

            var converter = new BinaryConverter();

            byte[] bytes = converter.Serialize(obj);
            StreamMessage fromBytes = converter.Deserialize<StreamMessage>(bytes);

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

            public StreamMessage()
            {
            }
        }
    }
}

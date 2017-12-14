using System;
using FluentAssertions;
using Xunit;

namespace BinaryFormatterTests.Bugs
{
    /// <summary>
    /// https://github.com/lukasz-pyrzyk/BinaryFormatter/issues/71
    /// </summary>
    public class DeserializingObjectWithByteDataIsFailing
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
            fromBytes.StreamName.Should().Be(obj.StreamName);
            fromBytes.StreamType.Should().Be(obj.StreamType);
            fromBytes.StreamSize.Should().Be(obj.StreamSize);
            fromBytes.StreamContent.Should().Be(obj.StreamContent);
        }

        [Serializable]
        private class StreamMessage : Message
        {
            public string StreamName { get; set; }
            public int StreamType { get; set; }
            public float StreamSize { get; set; }
            public byte StreamContent { get; set; }

            public StreamMessage()
            {
                MessageType = 55;
            }
        }

        [Serializable]
        private abstract class Message
        {
            public int MessageType;

            public override string ToString()
            {
                return string.Format("[Message: Type={0}]", MessageType);
            }
        }
    }
}

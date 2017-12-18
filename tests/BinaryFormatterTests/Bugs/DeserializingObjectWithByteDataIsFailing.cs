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
            fromBytes.StreamContent.Should().Be(obj.StreamContent);
            fromBytes.Bytes.Should().Equal(obj.Bytes);
        }

        [Serializable]
        private class StreamMessage
        {
            public byte StreamContent { get; set; }
            public byte[] Bytes { get; set; }
        }
    }
}

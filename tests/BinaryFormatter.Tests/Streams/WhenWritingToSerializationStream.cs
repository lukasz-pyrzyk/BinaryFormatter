using System;
using System.IO;
using System.Linq;
using System.Text;
using BinaryFormatter.Streams;
using FluentAssertions;
using Xunit;

namespace BinaryFormatter.Tests.Streams
{
    public class WhenWritingToSerializationStream
    {
        [Fact]
        public void Write_WritesAllElementsToTheStream()
        {
            // Arrange
            var stream = new MemoryStream();
            var serializationStream = new SerializationStream(stream);
            var data = new byte[100];
            serializationStream.Write(data);

            // Act
            byte[] dataFromStream = stream.ToArray();

            // Assert
            data.Should().Equal(dataFromStream);
        }

        [Fact]
        public void WriteWithLengthPrefix_WritesAllElementsToTheStream()
        {
            // Arrange
            var stream = new MemoryStream();
            var serializationStream = new SerializationStream(stream);
            var data = Encoding.UTF8.GetBytes("Hello world");
            var expectedBytesHeader = BitConverter.GetBytes(data.Length);

            // Act
            serializationStream.WriteWithLengthPrefix(data);

            // Assert
            byte[] dataFromStream = stream.ToArray();
            dataFromStream.Should().HaveCount(data.Length + sizeof(int));

            byte[] lengthPrefix = dataFromStream.Take(sizeof(int)).ToArray();
            expectedBytesHeader.Should().Equal(lengthPrefix);

            byte[] dataBytes = dataFromStream.Skip(sizeof(int)).ToArray();
            data.Should().Equal(dataBytes);
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Text;
using BinaryFormatter.Utils;
using Xunit;

namespace BinaryFormatter.Tests.Utils
{
    public class StreamExtensionsTests
    {
        [Fact]
        public void Write_WritesAllElementsToTheStream()
        {
            var stream = new MemoryStream();
            var data = new byte[100];
            stream.Write(data);

            byte[] dataFromStream = stream.ToArray();

            Assert.Equal(data, dataFromStream);
        }

        [Fact]
        public void WriteWithLengthPrefix_WritesAllElementsToTheStream()
        {
            // Arrange
            var stream = new MemoryStream();
            var data = Encoding.UTF8.GetBytes("Hello world");
            var expectedBytesHeader = BitConverter.GetBytes(data.Length);

            // Act
            stream.WriteWithLengthPrefix(data);
            
            // Assert
            byte[] dataFromStream = stream.ToArray();
            Assert.Equal(dataFromStream.Length, data.Length + sizeof(int));

            byte[] lengthPrefix = dataFromStream.Take(sizeof(int)).ToArray();
            Assert.Equal(expectedBytesHeader, lengthPrefix);

            byte[] dataBytes = dataFromStream.Skip(sizeof(int)).ToArray();
            Assert.Equal(data, dataBytes);
        }
    }
}

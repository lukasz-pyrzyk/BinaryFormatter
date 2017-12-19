using System;
using System.Text;
using BinaryFormatter;
using FluentAssertions;
using Xunit;

namespace BinaryFormatterTests
{
    public class WorkingStreamTests
    {
        [Theory]
        [InlineData("System.String", typeof(string))]
        public void CanResolveTypeWithUTF8(string typeName, Type expectedType)
        {
            // arrange
            byte[] data = WriteTypeWithLengthPrefix(typeName);
            var stream = new WorkingStream(data);
            
            // Act
            Type type = stream.ReadType();

            // Assert
            expectedType.Should().Be(type);
        }

        private static byte[] WriteTypeWithLengthPrefix(string typeName)
        {
            byte[] typeInfo = Encoding.UTF8.GetBytes(typeName);
            byte[] sizeBytes = BitConverter.GetBytes(typeInfo.Length);
            byte[] data = new byte[sizeBytes.Length + typeInfo.Length];
            Array.Copy(sizeBytes, 0, data, 0, sizeBytes.Length);
            Array.Copy(typeInfo, 0, data, sizeBytes.Length, typeInfo.Length);
            return data;
        }
    }
}

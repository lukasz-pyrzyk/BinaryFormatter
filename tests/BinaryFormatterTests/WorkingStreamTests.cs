using System;
using System.Text;
using BinaryFormatter;
using FluentAssertions;
using Xunit;

namespace BinaryFormatterTests
{
    public class WorkingStreamTests
    {
        [Fact]
        public void Ends_WhenOffsetIsEqualStreamLength()
        {
            // Arrange
            var data = new byte[64];

            // Act
            var stream = new WorkingStream(data, data.Length);

            // Assert
            stream.HasEnded.Should().BeTrue();
        }

        [Fact]
        public void NotEnds_WhenOffsetIsSmallerThanData()
        {
            // Arrange
            var data = new byte[64];

            // Act
            var stream = new WorkingStream(data, data.Length - 1);

            // Assert
            stream.HasEnded.Should().BeFalse();
        }

        [Fact]
        public void OffsetCanBeChanged()
        {
            // Arrange
            var data = new byte[64];
            const int newIndex = 2;

            // Act
            var stream = new WorkingStream(data);
            stream.ChangeOffset(newIndex);

            // Assert
            stream.Offset.Should().Be(newIndex);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void BooleanCanBeReaded(bool value)
        {
            // Arrange
            var data = BitConverter.GetBytes(value);

            // Act
            var stream = new WorkingStream(data);
            var result = stream.ReadBool();

            // Assert
            result.Should().Be(value);
            stream.Offset.Should().Be(sizeof(bool));
        }

        [Theory]
        [InlineData((byte)1)]
        [InlineData(byte.MaxValue)]
        [InlineData(byte.MinValue)]
        public void ByteCanBeReaded(byte value)
        {
            // Arrange
            var data = BitConverter.GetBytes(value);

            // Act
            var stream = new WorkingStream(data);
            var result = stream.ReadByte();

            // Assert
            result.Should().Be(value);
            stream.Offset.Should().Be(sizeof(byte));
        }

        [Theory]
        [InlineData((sbyte)1)]
        [InlineData(sbyte.MaxValue)]
        [InlineData(sbyte.MinValue)]
        public void SByteCanBeReaded(sbyte value)
        {
            // Arrange
            var data = BitConverter.GetBytes(value);

            // Act
            var stream = new WorkingStream(data);
            var result = stream.ReadSByte();

            // Assert
            result.Should().Be(value);
            stream.Offset.Should().Be(sizeof(sbyte));
        }

        [Theory]
        [InlineData((char)1)]
        [InlineData(char.MaxValue)]
        [InlineData(char.MinValue)]
        public void CharCanBeReaded(char value)
        {
            // Arrange
            var data = BitConverter.GetBytes(value);

            // Act
            var stream = new WorkingStream(data);
            var result = stream.ReadChar();

            // Assert
            result.Should().Be(value);
            stream.Offset.Should().Be(sizeof(char));
        }

        [Theory]
        [InlineData((ushort)1)]
        [InlineData(ushort.MaxValue)]
        [InlineData(ushort.MinValue)]
        public void UShortCanBeReaded(ushort value)
        {
            // Arrange
            var data = BitConverter.GetBytes(value);

            // Act
            var stream = new WorkingStream(data);
            var result = stream.ReadUShort();

            // Assert
            result.Should().Be(value);
            stream.Offset.Should().Be(sizeof(ushort));
        }

        [Theory]
        [InlineData((short)1)]
        [InlineData(short.MaxValue)]
        [InlineData(short.MinValue)]
        public void ShortCanBeReaded(short value)
        {
            // Arrange
            var data = BitConverter.GetBytes(value);

            // Act
            var stream = new WorkingStream(data);
            var result = stream.ReadShort();

            // Assert
            result.Should().Be(value);
            stream.Offset.Should().Be(sizeof(short));
        }

        [Theory]
        [InlineData((uint)1)]
        [InlineData(uint.MaxValue)]
        [InlineData(uint.MinValue)]
        public void UIntCanBeReaded(uint value)
        {
            // Arrange
            var data = BitConverter.GetBytes(value);

            // Act
            var stream = new WorkingStream(data);
            var result = stream.ReadUInt();

            // Assert
            result.Should().Be(value);
            stream.Offset.Should().Be(sizeof(uint));
        }

        [Theory]
        [InlineData((int)1)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void IntCanBeReaded(int value)
        {
            // Arrange
            var data = BitConverter.GetBytes(value);

            // Act
            var stream = new WorkingStream(data);
            var result = stream.ReadInt();

            // Assert
            result.Should().Be(value);
            stream.Offset.Should().Be(sizeof(int));
        }

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

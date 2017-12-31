using System;
using System.Collections.Generic;
using System.Text;
using BinaryFormatter.Streams;
using FluentAssertions;
using Xunit;

namespace BinaryFormatter.Tests.Streams
{
    public class WhenReadingFromDeserializationStream
    {
        [Fact]
        public void Ends_WhenOffsetIsEqualStreamLength()
        {
            // Arrange
            var data = new byte[64];

            // Act
            var stream = new DeserializationStream(data, data.Length);

            // Assert
            stream.HasEnded.Should().BeTrue();
        }

        [Fact]
        public void Ends_WhenItsEmpty()
        {
            // Arrange
            var data = new byte[0];

            // Act
            var stream = new DeserializationStream(data);

            // Assert
            stream.HasEnded.Should().BeTrue();
        }

        [Fact]
        public void NotEnds_WhenOffsetIsSmallerThanData()
        {
            // Arrange
            var data = new byte[64];

            // Act
            var stream = new DeserializationStream(data, data.Length - 1);

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
            var stream = new DeserializationStream(data);
            stream.SetOffset(newIndex);

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
            var stream = new DeserializationStream(data);
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
            var stream = new DeserializationStream(data);
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
            var stream = new DeserializationStream(data);
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
            var stream = new DeserializationStream(data);
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
            var stream = new DeserializationStream(data);
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
            var stream = new DeserializationStream(data);
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
            var stream = new DeserializationStream(data);
            var result = stream.ReadUInt();

            // Assert
            result.Should().Be(value);
            stream.Offset.Should().Be(sizeof(uint));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void IntCanBeReaded(int value)
        {
            // Arrange
            var data = BitConverter.GetBytes(value);

            // Act
            var stream = new DeserializationStream(data);
            var result = stream.ReadInt();

            // Assert
            result.Should().Be(value);
            stream.Offset.Should().Be(sizeof(int));
        }

        [Theory]
        [InlineData((float)1)]
        [InlineData(float.MaxValue)]
        [InlineData(float.MinValue)]
        public void FloatCanBeReaded(float value)
        {
            // Arrange
            var data = BitConverter.GetBytes(value);

            // Act
            var stream = new DeserializationStream(data);
            var result = stream.ReadFloat();

            // Assert
            result.Should().Be(value);
            stream.Offset.Should().Be(sizeof(float));
        }

        [Theory]
        [InlineData((double)1)]
        [InlineData(double.MaxValue)]
        [InlineData(double.MinValue)]
        public void DoubleCanBeReaded(double value)
        {
            // Arrange
            var data = BitConverter.GetBytes(value);

            // Act
            var stream = new DeserializationStream(data);
            var result = stream.ReadDouble();

            // Assert
            result.Should().Be(value);
            stream.Offset.Should().Be(sizeof(double));
        }

        [Theory]
        [InlineData((ulong)1)]
        [InlineData(ulong.MaxValue)]
        [InlineData(ulong.MinValue)]
        public void ULongCanBeReaded(ulong value)
        {
            // Arrange
            var data = BitConverter.GetBytes(value);

            // Act
            var stream = new DeserializationStream(data);
            var result = stream.ReadULong();

            // Assert
            result.Should().Be(value);
            stream.Offset.Should().Be(sizeof(ulong));
        }

        [Theory]
        [InlineData((long)1)]
        [InlineData(long.MaxValue)]
        [InlineData(long.MinValue)]
        public void LongCanBeReaded(long value)
        {
            // Arrange
            var data = BitConverter.GetBytes(value);

            // Act
            var stream = new DeserializationStream(data);
            var result = stream.ReadLong();

            // Assert
            result.Should().Be(value);
            stream.Offset.Should().Be(sizeof(long));
        }

        [Fact]
        public void ByteArrayWithLengthPrefixCanBeReaded()
        {
            // Arrange
            var data = Encoding.UTF8.GetBytes("hello world");
            var size = BitConverter.GetBytes(data.Length);
            var finalData = new List<byte>();
            foreach (byte b in size)
            {
                finalData.Add(b);
            }

            foreach (byte b in data)
            {
                finalData.Add(b);
            }

            // Act
            var stream = new DeserializationStream(finalData.ToArray());
            var result = stream.ReadBytesWithSizePrefix();

            // Assert
            result.Should().Equal(data);
            stream.Offset.Should().Be(finalData.Count);
        }

        [Fact]
        public void ByteArrayWithoutLengthPrefixCanBeReaded()
        {
            // Arrange
            var data = Encoding.UTF8.GetBytes("hello world");

            // Act
            var stream = new DeserializationStream(data);
            var result = stream.ReadBytes(data.Length);

            // Assert
            result.Should().Equal(data);
            stream.Offset.Should().Be(data.Length);
        }

        [Fact]
        public void StringInUtf8CanBeReaded()
        {
            // Arrange
            const string s = "hello world";
            var data = Encoding.UTF8.GetBytes(s);
            var size = BitConverter.GetBytes(data.Length);
            var finalData = new List<byte>();
            foreach (byte b in size)
            {
                finalData.Add(b);
            }

            foreach (byte b in data)
            {
                finalData.Add(b);
            }

            // Act
            var stream = new DeserializationStream(finalData.ToArray());
            var result = stream.ReadUtf8WithSizePrefix();

            // Assert
            result.Should().Be(s);
            stream.Offset.Should().Be(finalData.Count);
        }

        [Theory]
        [InlineData("System.String", typeof(string))]
        public void TypeCanReaded(string typeName, Type expectedType)
        {
            // arrange
            byte[] typeInfo = Encoding.UTF8.GetBytes(typeName);
            byte[] sizeBytes = BitConverter.GetBytes(typeInfo.Length);
            byte[] data = new byte[sizeBytes.Length + typeInfo.Length];
            Array.Copy(sizeBytes, 0, data, 0, sizeBytes.Length);
            Array.Copy(typeInfo, 0, data, sizeBytes.Length, typeInfo.Length);

            // Act
            var stream = new DeserializationStream(data);
            Type type = stream.ReadType();

            // Assert
            type.Should().Be(expectedType);
            stream.Offset.Should().Be(data.Length);
        }
    }
}

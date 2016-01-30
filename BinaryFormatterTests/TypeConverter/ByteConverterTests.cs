using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class ByteConverterTests
    {
        [Fact]
        public void CanCorrectSerialize()
        {
            byte value = byte.MaxValue;
            ByteConverter converter = new ByteConverter();
            byte[] bytes = converter.Serialize(value);

            int size = BitConverter.ToInt32(bytes, 0);
            int valueFromBytes = BitConverter.ToInt16(bytes, sizeof (int));

            Assert.Equal(size, sizeof(byte));
            Assert.Equal(valueFromBytes, value);
        }
    }
}

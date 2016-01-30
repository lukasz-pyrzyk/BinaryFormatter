using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class UIntConverterTests
    {
        [Fact]
        public void CanCorrectSerialize()
        {
            uint value = uint.MaxValue;
            BaseTypeConverter converter = new UIntConverter();
            byte[] bytes = converter.Serialize(value);

            int size = BitConverter.ToInt32(bytes, 0);
            uint valueFromBytes = BitConverter.ToUInt32(bytes, sizeof (int));

            Assert.Equal(size, sizeof(uint));
            Assert.Equal(valueFromBytes, value);
        }
    }
}

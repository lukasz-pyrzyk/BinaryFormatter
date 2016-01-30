using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class ULongConverterTests
    {
        [Fact]
        public void CanCorrectSerialize()
        {
            ulong value = ulong.MaxValue;
            ULongConverter converter = new ULongConverter();
            byte[] bytes = converter.Serialize(value);

            int size = BitConverter.ToInt32(bytes, 0);
            ulong valueFromBytes = BitConverter.ToUInt64(bytes, sizeof (int));

            Assert.Equal(size, sizeof(ulong));
            Assert.Equal(valueFromBytes, value);
        }
    }
}

using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class LongConverterTests
    {
        [Fact]
        public void CanCorrectSerialize()
        {
            long value = long.MaxValue;
            LongConverter converter = new LongConverter();
            byte[] bytes = converter.Serialize(value);

            int size = BitConverter.ToInt32(bytes, 0);
            long valueFromBytes = BitConverter.ToInt64(bytes, sizeof (int));

            Assert.Equal(size, sizeof(long));
            Assert.Equal(valueFromBytes, value);
        }
    }
}

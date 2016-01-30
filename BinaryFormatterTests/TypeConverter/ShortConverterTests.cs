using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class ShortConverterTests
    {
        [Fact]
        public void CanCorrectSerialize()
        {
            short value = short.MaxValue;
            ShortConverter converter = new ShortConverter();
            byte[] bytes = converter.Serialize(value);

            int size = BitConverter.ToInt32(bytes, 0);
            short valueFromBytes = BitConverter.ToInt16(bytes, sizeof (int));

            Assert.Equal(size, sizeof(short));
            Assert.Equal(valueFromBytes, value);
        }
    }
}

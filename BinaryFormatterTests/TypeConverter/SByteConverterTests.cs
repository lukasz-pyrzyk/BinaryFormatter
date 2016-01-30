using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class SByteConverterTests
    {
        [Fact]
        public void CanCorrectSerializeShort()
        {
            sbyte value = sbyte.MaxValue;
            SByteConverter converter = new SByteConverter();
            byte[] bytes = converter.Serialize(value);

            int size = BitConverter.ToInt32(bytes, 0);
            int valueFromBytes = BitConverter.ToInt16(bytes, sizeof (int));

            Assert.Equal(size, sizeof(sbyte));
            Assert.Equal(valueFromBytes, value);
        }
    }
}

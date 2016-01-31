using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class LongConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            long value = long.MaxValue;
            LongConverter converter = new LongConverter();
            byte[] bytes = converter.Serialize(value);

            long valueFromBytes = converter.Deserialize(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

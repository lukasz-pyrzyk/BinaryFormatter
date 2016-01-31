using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class SByteConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            sbyte value = sbyte.MaxValue;
            SByteConverter converter = new SByteConverter();
            byte[] bytes = converter.Serialize(value);

            sbyte valueFromBytes = converter.Deserialize(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class ByteConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            byte value = byte.MaxValue;
            ByteConverter converter = new ByteConverter();
            byte[] bytes = converter.Serialize(value);

            byte valueFromBytes = converter.Deserialize(bytes);
            
            Assert.Equal(valueFromBytes, value);
        }
    }
}

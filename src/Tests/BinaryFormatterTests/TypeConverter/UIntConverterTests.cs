using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class UIntConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            uint value = uint.MaxValue;
            UIntConverter converter = new UIntConverter();
            byte[] bytes = converter.Serialize(value);

            uint valueFromBytes = converter.Deserialize(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

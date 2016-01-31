using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class UShortConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            ushort value = ushort.MaxValue;
            UShortConverter converter = new UShortConverter();
            byte[] bytes = converter.Serialize(value);

            ushort valueFromBytes = converter.Deserialize(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

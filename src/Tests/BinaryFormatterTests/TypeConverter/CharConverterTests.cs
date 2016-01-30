using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class CharConverterTests
    {
        [Fact]
        public void CanCorrectSerialize()
        {
            char value = char.MaxValue;
            CharConverter converter = new CharConverter();
            byte[] bytes = converter.Serialize(value);

            int size = BitConverter.ToInt32(bytes, 0);
            char valueFromBytes = BitConverter.ToChar(bytes, sizeof (int));

            Assert.Equal(size, sizeof(char));
            Assert.Equal(valueFromBytes, value);
        }
    }
}

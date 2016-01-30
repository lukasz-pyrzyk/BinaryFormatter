using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class BoolConverterTests
    {
        [Fact]
        public void CanCorrectSerialize()
        {
            bool value = false;
            BoolConverter converter = new BoolConverter();
            byte[] bytes = converter.Serialize(value);

            int size = BitConverter.ToInt32(bytes, 0);
            bool valueFromBytes = BitConverter.ToBoolean(bytes, sizeof (int));

            Assert.Equal(size, sizeof(bool));
            Assert.Equal(valueFromBytes, value);
        }
    }
}

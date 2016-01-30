using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class IntConverterTests
    {
        [Fact]
        public void CanCorrectSerialize()
        {
            int value = int.MaxValue;
            IntConverter converter = new IntConverter();
            byte[] bytes = converter.Serialize(value);

            int size = BitConverter.ToInt32(bytes, 0);
            Int32 valueFromBytes = BitConverter.ToInt32(bytes, sizeof (int));

            Assert.Equal(size, sizeof(int));
            Assert.Equal(valueFromBytes, value);
        }
    }
}

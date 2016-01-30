using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class DoubleConverterTests
    {
        [Fact]
        public void CanCorrectSerialize()
        {
            double value = double.MaxValue;
            DoubleConverter converter = new DoubleConverter();
            byte[] bytes = converter.Serialize(value);

            int size = BitConverter.ToInt32(bytes, 0);
            double valueFromBytes = BitConverter.ToDouble(bytes, sizeof (int));

            Assert.Equal(size, sizeof(double));
            Assert.Equal(valueFromBytes, value);
        }
    }
}

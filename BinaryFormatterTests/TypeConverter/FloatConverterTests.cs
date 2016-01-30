using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class FloatConverterTests
    {
        [Fact]
        public void CanCorrectSerialize()
        {
            float value = float.MaxValue;
            FloatConverter converter = new FloatConverter();
            byte[] bytes = converter.Serialize(value);

            int size = BitConverter.ToInt32(bytes, 0);
            float valueFromBytes = BitConverter.ToSingle(bytes, sizeof (int));

            Assert.Equal(size, sizeof(float));
            Assert.Equal(valueFromBytes, value);
        }
    }
}

using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class FloatConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            float value = float.MaxValue;
            FloatConverter converter = new FloatConverter();
            byte[] bytes = converter.Serialize(value);

            float valueFromBytes = converter.Deserialize(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

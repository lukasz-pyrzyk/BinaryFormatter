using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class FloatConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            float value = float.MaxValue;
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            float valueFromBytes = converter.Deserialize<float>(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

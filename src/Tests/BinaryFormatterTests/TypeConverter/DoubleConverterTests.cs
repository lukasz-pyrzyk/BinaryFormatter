using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class DoubleConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            double value = double.MaxValue;
            DoubleConverter converter = new DoubleConverter();
            byte[] bytes = converter.Serialize(value);

            double valueFromBytes = converter.Deserialize(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

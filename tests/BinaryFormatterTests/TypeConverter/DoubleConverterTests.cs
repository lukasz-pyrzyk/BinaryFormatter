using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class DoubleConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            double value = double.MaxValue;
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            double valueFromBytes = converter.Deserialize<double>(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

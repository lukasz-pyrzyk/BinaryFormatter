using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class DecimalConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            decimal value = decimal.MaxValue;
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            decimal valueFromBytes = converter.Deserialize<decimal>(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

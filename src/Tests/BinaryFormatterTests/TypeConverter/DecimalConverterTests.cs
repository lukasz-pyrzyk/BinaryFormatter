using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class DecimalConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            decimal value = decimal.MaxValue;
            DecimalConverter converter = new DecimalConverter();
            byte[] bytes = converter.Serialize(value);

            decimal valueFromBytes = converter.Deserialize(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

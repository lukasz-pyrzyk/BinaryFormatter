using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class CharConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            char value = char.MaxValue;
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            char valueFromBytes = converter.Deserialize<char>(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

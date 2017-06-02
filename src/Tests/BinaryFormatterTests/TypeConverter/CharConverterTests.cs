using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class CharConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            char value = char.MaxValue;
            CharConverter converter = new CharConverter();
            byte[] bytes = converter.Serialize(value);

            char valueFromBytes = converter.Deserialize(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

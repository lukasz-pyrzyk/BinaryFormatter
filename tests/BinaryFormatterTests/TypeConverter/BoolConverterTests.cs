using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class BoolConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            bool value = false;
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            bool valueFromBytes = converter.Deserialize<bool>(bytes);

            Assert.Equal(value, valueFromBytes);
        }
    }
}

using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class BoolConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            bool value = false;
            BoolConverter converter = new BoolConverter();
            byte[] bytes = converter.Serialize(value);

            bool valueFromBytes = converter.Deserialize(bytes);

            Assert.Equal(value, valueFromBytes);
        }
    }
}

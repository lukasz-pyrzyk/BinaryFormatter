using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class StringConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            string value = "Lorem ipsum";
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            string valueFromBytes = converter.Deserialize<string>(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

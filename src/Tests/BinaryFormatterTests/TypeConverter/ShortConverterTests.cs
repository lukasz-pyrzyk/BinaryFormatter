using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class ShortConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            short value = short.MaxValue;
            ShortConverter converter = new ShortConverter();
            byte[] bytes = converter.Serialize(value);

            short valueFromBytes = converter.Deserialize(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

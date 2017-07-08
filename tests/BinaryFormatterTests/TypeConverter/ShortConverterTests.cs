using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class ShortConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            short value = short.MaxValue;
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            short valueFromBytes = converter.Deserialize<short>(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

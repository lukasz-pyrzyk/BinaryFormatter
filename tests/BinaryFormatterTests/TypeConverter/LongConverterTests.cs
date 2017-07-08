using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class LongConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            long value = long.MaxValue;
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            long valueFromBytes = converter.Deserialize<long>(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

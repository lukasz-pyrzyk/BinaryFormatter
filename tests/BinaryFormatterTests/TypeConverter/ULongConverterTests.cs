using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class ULongConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            ulong value = ulong.MaxValue;
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            ulong valueFromBytes = converter.Deserialize<ulong>(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

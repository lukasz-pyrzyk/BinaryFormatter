using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class UIntConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            uint value = uint.MaxValue;
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            uint valueFromBytes = converter.Deserialize<uint>(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

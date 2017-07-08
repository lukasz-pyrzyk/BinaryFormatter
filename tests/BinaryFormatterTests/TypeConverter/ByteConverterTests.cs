using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class ByteConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            byte value = byte.MaxValue;
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            byte valueFromBytes = converter.Deserialize<byte>(bytes);
            
            Assert.Equal(valueFromBytes, value);
        }
    }
}

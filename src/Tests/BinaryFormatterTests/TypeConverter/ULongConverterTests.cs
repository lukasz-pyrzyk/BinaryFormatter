using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class ULongConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            ulong value = ulong.MaxValue;
            ULongConverter converter = new ULongConverter();
            byte[] bytes = converter.Serialize(value);

            ulong valueFromBytes = converter.Deserialize(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class UShortConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            ushort value = ushort.MaxValue;
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            ushort valueFromBytes = converter.Deserialize<ushort>(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

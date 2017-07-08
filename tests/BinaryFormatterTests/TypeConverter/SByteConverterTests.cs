using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class SByteConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            sbyte value = sbyte.MaxValue;
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            sbyte valueFromBytes = converter.Deserialize<sbyte>(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class IntConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            int value = int.MaxValue;
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            int valueFromBytes = converter.Deserialize<int>(bytes);
            
            Assert.Equal(valueFromBytes, value);
        }
    }
}

using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class IntConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            int value = int.MaxValue;
            IntConverter converter = new IntConverter();
            byte[] bytes = converter.Serialize(value);

            int valueFromBytes = converter.Deserialize(bytes);
            
            Assert.Equal(valueFromBytes, value);
        }
    }
}

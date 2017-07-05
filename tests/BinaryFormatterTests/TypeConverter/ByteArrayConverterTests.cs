using System.Text;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class ByteArrayConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            byte[] value = Encoding.UTF8.GetBytes("lorem ipsum");
            ByteArrayConverter converter = new ByteArrayConverter();
            byte[] bytes = converter.Serialize(value);

            byte[] valueFromBytes = converter.Deserialize(bytes);
            
            Assert.Equal(valueFromBytes, value);
        }
    }
}

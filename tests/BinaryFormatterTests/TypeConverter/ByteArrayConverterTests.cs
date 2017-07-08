using System.Text;
using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class ByteArrayConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            byte[] value = Encoding.UTF8.GetBytes("lorem ipsum");
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            byte[] valueFromBytes = converter.Deserialize<byte[]>(bytes);
            
            Assert.Equal(valueFromBytes, value);
        }
    }
}

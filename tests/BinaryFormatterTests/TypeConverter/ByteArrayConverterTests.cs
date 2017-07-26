using System.Text;
using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class ByteArrayConverterTests : ConverterTest<byte[]>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override byte[] Value => Encoding.UTF8.GetBytes("lorem ipsum");
    }
}

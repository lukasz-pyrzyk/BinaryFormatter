using System.Text;
using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
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

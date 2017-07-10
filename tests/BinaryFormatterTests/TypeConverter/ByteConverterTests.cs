using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class ByteConverterTests : ConverterTest<byte>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override byte Value => byte.MaxValue;
    }
}

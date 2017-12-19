using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
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

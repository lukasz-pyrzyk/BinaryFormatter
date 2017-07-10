using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class UIntConverterTests : ConverterTest<uint>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override uint Value => uint.MaxValue;
    }
}

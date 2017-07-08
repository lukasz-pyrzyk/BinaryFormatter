using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class BoolConverterTests : ConverterTest<bool>
    {
        public override bool Value => true;

        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }
    }
}

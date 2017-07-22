using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class NullConverterTests : ConverterTest<object>
    {
        public override object Value => null;

        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }
    }
}

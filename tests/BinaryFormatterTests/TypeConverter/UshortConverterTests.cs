using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class UShortConverterTests : ConverterTest<ushort>

    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override ushort Value => ushort.MaxValue;
    }
}

using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
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

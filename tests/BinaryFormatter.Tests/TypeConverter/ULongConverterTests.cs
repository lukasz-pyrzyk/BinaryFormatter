using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class ULongConverterTests : ConverterTest<ulong>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override ulong Value => ulong.MaxValue;
    }
}

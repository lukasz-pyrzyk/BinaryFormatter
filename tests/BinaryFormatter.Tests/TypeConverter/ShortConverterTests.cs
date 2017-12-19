using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class ShortConverterTests : ConverterTest<short>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override short Value => short.MaxValue;
    }
}

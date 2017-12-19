using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class DecimalConverterTests : ConverterTest<decimal>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override decimal Value => decimal.MaxValue;
    }
}

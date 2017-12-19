using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class SByteConverterTests : ConverterTest<sbyte>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override sbyte Value => sbyte.MaxValue;
    }
}

using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class LongConverterTests : ConverterTest<long>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override long Value => long.MaxValue;
    }
}

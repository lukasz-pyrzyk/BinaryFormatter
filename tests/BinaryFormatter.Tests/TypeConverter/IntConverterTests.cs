using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class IntConverterTests : ConverterTest<int>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override int Value => int.MaxValue;
    }
}

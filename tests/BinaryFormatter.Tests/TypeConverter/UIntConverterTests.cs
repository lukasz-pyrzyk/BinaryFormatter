using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class UIntConverterTests : ConverterTest<uint>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override uint Value => uint.MaxValue;
    }
}

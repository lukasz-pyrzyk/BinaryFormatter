using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class BoolConverterTests : ConverterTest<bool>
    {
        public override bool Value => true;

        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }
    }
}

using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class CharConverterTests : ConverterTest<char>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override char Value => char.MaxValue;
    }
}

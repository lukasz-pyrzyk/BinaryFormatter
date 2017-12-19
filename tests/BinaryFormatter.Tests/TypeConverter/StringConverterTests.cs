using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class StringConverterTests : ConverterTest<string>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override string Value => "lorem ipsum";
    }
}

using System.Text;
using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
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

using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class DoubleConverterTests : ConverterTest<double>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override double Value => double.MaxValue;
    }
}

using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class FloatConverterTests : ConverterTest<float>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override float Value => float.MaxValue;
    }
}

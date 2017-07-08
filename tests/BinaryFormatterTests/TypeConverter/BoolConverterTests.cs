using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class BoolConverterTests : BaseTest<bool>
    {
        public override bool Value => true;
    }

    public abstract class BaseTest<T>
    {
        public abstract T Value { get; }

        [Fact]
        public void CanSerializeAndDeserialize()
        {
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(Value);

            T after = converter.Deserialize<T>(bytes);
            Assert.Equal(Value, after);
        }
    }
}

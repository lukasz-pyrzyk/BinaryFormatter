using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public abstract class ConverterTest<T>
    {
        public abstract T Value { get; }

        protected void RunTest()
        {
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(Value);

            T after = converter.Deserialize<T>(bytes);
            Assert.Equal(Value, after);
        }
    }
}
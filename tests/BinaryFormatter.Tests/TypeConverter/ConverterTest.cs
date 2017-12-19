using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public abstract class ConverterTest<T>
    {
        public abstract T Value { get; }

        protected void RunTest()
        {
            T after = TestHelper.SerializeAndDeserialize(Value);
            Assert.Equal(Value, after);
        }
    }
}
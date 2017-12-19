using FluentAssertions;

namespace BinaryFormatter.Tests.TypeConverter
{
    public abstract class ConverterTest<T>
    {
        public abstract T Value { get; }

        protected void RunTest()
        {
            T deserialized = TestHelper.SerializeAndDeserialize(Value);
            deserialized.Should().Be(Value);
        }
    }
}
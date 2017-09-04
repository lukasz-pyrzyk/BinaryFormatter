using System;
using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class DateTimeOffsetConverterTests : ConverterTest<DateTimeOffset>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override DateTimeOffset Value => DateTimeOffset.MaxValue;
    }
}

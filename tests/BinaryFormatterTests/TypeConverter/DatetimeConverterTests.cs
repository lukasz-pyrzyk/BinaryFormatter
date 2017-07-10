using System;
using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class DatetimeConverterTests : ConverterTest<DateTime>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override DateTime Value => DateTime.MaxValue;
    }
}

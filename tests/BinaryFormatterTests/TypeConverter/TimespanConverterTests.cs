using System;
using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class TimespanConverterTests : ConverterTest<TimeSpan>
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public override TimeSpan Value => TimeSpan.MaxValue;
    }
}

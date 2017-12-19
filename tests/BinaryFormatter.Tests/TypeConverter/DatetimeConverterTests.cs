using System;
using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
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

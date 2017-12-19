using System;
using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
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

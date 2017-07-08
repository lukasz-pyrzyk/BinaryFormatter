using System;
using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class DatetimeConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            DateTime value = DateTime.MaxValue;
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);

            DateTime valueFromBytes = converter.Deserialize<DateTime>(bytes);
            
            Assert.Equal(valueFromBytes, value);
        }
    }
}

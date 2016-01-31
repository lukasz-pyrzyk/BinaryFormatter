using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class DatetimeConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            DateTime value = DateTime.MaxValue;
            DatetimeConverter converter = new DatetimeConverter();
            byte[] bytes = converter.Serialize(value);

            DateTime valueFromBytes = converter.Deserialize(bytes);
            
            Assert.Equal(valueFromBytes, value);
        }
    }
}

using System;
using System.Text;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class DecimalConverterTests
    {
        [Fact]
        public void CanCorrectSerialize()
        {
            decimal value = decimal.MaxValue;
            DecimalConverter converter = new DecimalConverter();
            byte[] bytes = converter.Serialize(value);

            int size = BitConverter.ToInt32(bytes, 0);
            string valuestring = Encoding.UTF8.GetString(bytes, sizeof(int), bytes.Length - sizeof(int));
            decimal valueFromBytes = decimal.Parse(valuestring);

            Assert.Equal(size, valuestring.Length);
            Assert.Equal(valueFromBytes, value);
        }
    }
}

using System;
using System.Text;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class StringConverterTests
    {
        [Fact]
        public void CanCorrectSerialize()
        {
            string value = "Lorem ipsum";
            StringConverter converter = new StringConverter();
            byte[] bytes = converter.Serialize(value);

            int size = BitConverter.ToInt32(bytes, 0);
            string valueFromBytes = Encoding.UTF8.GetString(bytes, sizeof(int), bytes.Length - sizeof(int));

            Assert.Equal(size, value.Length);
            Assert.Equal(valueFromBytes, value);
        }
    }
}

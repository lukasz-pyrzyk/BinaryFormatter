using System;
using System.Text;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class StringConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            string value = "Lorem ipsum";
            StringConverter converter = new StringConverter();
            byte[] bytes = converter.Serialize(value);

            string valueFromBytes = converter.Deserialize(bytes);

            Assert.Equal(valueFromBytes, value);
        }
    }
}

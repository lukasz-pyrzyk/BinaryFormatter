using System;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class UShortConverterTests
    {
        [Fact]
        public void CanCorrectSerializeUshort()
        {
            ushort value = ushort.MaxValue;
            UShortConverter converter = new UShortConverter();
            byte[] bytes = converter.Serialize(value);

            int size = BitConverter.ToInt32(bytes, 0);
            ushort valueFromBytes = BitConverter.ToUInt16(bytes, sizeof (int));

            Assert.Equal(size, sizeof(ushort));
            Assert.Equal(valueFromBytes, value);
        }
    }
}

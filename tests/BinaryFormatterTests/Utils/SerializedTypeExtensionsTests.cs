using BinaryFormatter.Utils;
using Xunit;
using BinaryFormatter.Types;

namespace BinaryFormatterTests.Utils
{
    public class SerializedTypeExtensionsTests
    {
        [Fact]
        public void IsBaseType()
        {
            Assert.True(SerializedType.Bool.IsBaseType());
            Assert.True(SerializedType.Byte.IsBaseType());
            Assert.True(SerializedType.ByteArray.IsBaseType());
            Assert.True(SerializedType.Char.IsBaseType());            
            Assert.True(SerializedType.Datetime.IsBaseType());
            Assert.True(SerializedType.Decimal.IsBaseType());
            Assert.True(SerializedType.Double.IsBaseType());
            Assert.True(SerializedType.Float.IsBaseType());            
            Assert.True(SerializedType.Int.IsBaseType());
            Assert.True(SerializedType.Long.IsBaseType());            
            Assert.True(SerializedType.Sbyte.IsBaseType());
            Assert.True(SerializedType.Short.IsBaseType());
            Assert.True(SerializedType.String.IsBaseType());
            Assert.True(SerializedType.Uint.IsBaseType());
            Assert.True(SerializedType.Ulong.IsBaseType());
            Assert.True(SerializedType.UShort.IsBaseType());

            Assert.False(SerializedType.CustomObject.IsBaseType());
            Assert.False(SerializedType.IEnumerable.IsBaseType());
            Assert.False(SerializedType.Null.IsBaseType());
        }
    }
}

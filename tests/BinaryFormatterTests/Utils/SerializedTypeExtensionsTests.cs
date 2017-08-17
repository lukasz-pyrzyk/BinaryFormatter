using BinaryFormatter.Utils;
using Xunit;
using BinaryFormatter.Types;

namespace BinaryFormatterTests.Utils
{
    public class SerializedTypeExtensionsTests
    {
        [Theory]
        [InlineData(SerializedType.Bool)]
        [InlineData(SerializedType.Byte)]
        [InlineData(SerializedType.ByteArray)]
        [InlineData(SerializedType.Char)]
        [InlineData(SerializedType.Datetime)]
        [InlineData(SerializedType.Decimal)]
        [InlineData(SerializedType.Double)]
        [InlineData(SerializedType.Float)]
        [InlineData(SerializedType.Int)]
        [InlineData(SerializedType.Long)]
        [InlineData(SerializedType.Sbyte)]
        [InlineData(SerializedType.Short)]
        [InlineData(SerializedType.String)]
        [InlineData(SerializedType.Uint)]
        [InlineData(SerializedType.Ulong)]
        [InlineData(SerializedType.UShort)]
        internal void IsBaseType(SerializedType type)
        {
            Assert.True(type.IsBaseType());
        }

        [Theory]
        [InlineData(SerializedType.CustomObject)]
        [InlineData(SerializedType.IEnumerable)]
        [InlineData(SerializedType.KeyValuePair)]
        [InlineData(SerializedType.Null)]
        internal void IsNotBaseType(SerializedType type)
        {
            Assert.False(type.IsBaseType());
        }
    }
}

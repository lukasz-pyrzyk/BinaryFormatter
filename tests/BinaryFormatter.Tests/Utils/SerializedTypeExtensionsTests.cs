﻿using BinaryFormatter.Types;
using BinaryFormatter.Utils;
using FluentAssertions;
using Xunit;

namespace BinaryFormatter.Tests.Utils
{
    public class SerializedTypeExtensionsTests
    {
        [Theory]
        [InlineData(SerializedType.Bool)]
        [InlineData(SerializedType.Byte)]
        [InlineData(SerializedType.ByteArray)]
        [InlineData(SerializedType.Char)]
        [InlineData(SerializedType.Datetime)]
        [InlineData(SerializedType.Timespan)]
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
        [InlineData(SerializedType.BigInteger)]
        internal void IsBaseType(SerializedType type)
        {
            type.IsBaseType().Should().BeTrue();
        }

        [Theory]
        [InlineData(SerializedType.CustomObject)]
        [InlineData(SerializedType.IEnumerable)]
        [InlineData(SerializedType.KeyValuePair)]
        [InlineData(SerializedType.Null)]
        internal void IsNotBaseType(SerializedType type)
        {
            type.IsBaseType().Should().BeFalse();
        }
    }
}

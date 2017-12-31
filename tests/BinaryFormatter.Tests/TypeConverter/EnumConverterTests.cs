using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
// ReSharper disable EnumUnderlyingTypeIsInt

namespace BinaryFormatter.Tests.TypeConverter
{
    public class EnumConverterTests
    {
        [Theory]
        [MemberData(nameof(TestCases))]
        public void CanSerializeAndDeserialize(Enum before)
        {
            var after = TestHelper.SerializeAndDeserialize(before);

            before.Should().Be(after);
        }

        public enum TestEnumByte : byte
        {
            Byte0 = 0x0,
            Byte1 = 0x1,
            Byte2 = 0x2,
            Byte4 = 0x4
        }
        public enum TestEnumSByte : sbyte
        {
            Sbyte0 = 0x0,
            Sbyte1 = 0x1,
            Sbyte2 = 0x2,
            Sbyte3 = 0x4
        }
        public enum TestEnumInt : int
        {
            Int0 = 0,
            Int1 = 1,
            Int2 = 2,
            Int3 = 3
        }
        public enum TestEnumUInt : uint
        {
            Uint0 = 0,
            Uint1 = 1,
            Uint2 = 2,
            Uint3 = 3
        }
        public enum TestEnumLong : long
        {
            Long0 = 0,
            Long1 = 1,
            Long2 = 2,
            Long3 = 3
        }
        public enum TestEnumULong : ulong
        {
            Ulong0 = 0,
            Ulong1 = 1,
            Ulong2 = 2,
            Ulong3 = 3
        }
        public enum TestEnumShort : short
        {
            Short0 = 0,
            Short1 = 1,
            Short2 = 2,
            Short3 = 3
        }
        public enum TestEnumUShort : ushort
        {
            Ushort0 = 0,
            Ushort1 = 1,
            Ushort2 = 2,
            Ushort3 = 3
        }

        public static IEnumerable<object[]> TestCases()
        {
            yield return new[] { (object)TestEnumInt.Int2 };
            yield return new[] { (object)TestEnumUInt.Uint3 };
            yield return new[] { (object)TestEnumShort.Short2 };
            yield return new[] { (object)TestEnumUShort.Ushort3 };
            yield return new[] { (object)TestEnumLong.Long2 };
            yield return new[] { (object)TestEnumULong.Ulong3 };
            yield return new[] { (object)TestEnumByte.Byte4 };
            yield return new[] { (object)TestEnumSByte.Sbyte2 };
        }
    }
}

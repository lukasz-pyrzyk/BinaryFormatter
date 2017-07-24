using System;
using System.Collections.Generic;
using Xunit;
using BinaryFormatter;

namespace BinaryFormatterTests.TypeConverter
{
    public class EnumConverterTests
    {
        BinaryConverter converter = new BinaryConverter();

        [Theory]
        [MemberData(nameof(TestCases))]
        public void CanSerializeAndDeserialize(Enum enumValue)
        {
            byte[] bytes = converter.Serialize(enumValue);
            var after = converter.Deserialize<object>(bytes);

            Assert.Equal(enumValue, after);
            Assert.Equal(enumValue.GetType(), after.GetType());
        }

        public enum TestEnum_Byte : byte
        {
            byte_0 = 0x0,
            byte_1 = 0x1,
            byte_2 = 0x2,
            byte_4 = 0x4
        }
        public enum TestEnum_SByte : sbyte
        {
            sbyte_0 = 0x0,
            sbyte_1 = 0x1,
            sbyte_2 = 0x2,
            sbyte_3 = 0x4
        }
        public enum TestEnum_Int : int
        {
            int_0 = 0,
            int_1 = 1,
            int_2 = 2,
            int_3 = 3
        }
        public enum TestEnum_UInt : uint
        {
            uint_0 = 0,
            uint_1 = 1,
            uint_2 = 2,
            uint_3 = 3
        }
        public enum TestEnum_Long : long
        {
            long_0 = 0,
            long_1 = 1,
            long_2 = 2,
            long_3 = 3
        }
        public enum TestEnum_ULong : ulong
        {
            ulong_0 = 0,
            ulong_1 = 1,
            ulong_2 = 2,
            ulong_3 = 3
        }
        public enum TestEnum_Short : short
        {
            short_0 = 0,
            short_1 = 1,
            short_2 = 2,
            short_3 = 3
        }
        public enum TestEnum_UShort : ushort
        {
            ushort_0 = 0,
            ushort_1 = 1,
            ushort_2 = 2,
            ushort_3 = 3
        }

        public static IEnumerable<object[]> TestCases()
        {
            yield return new[] { (object)TestEnum_Int.int_2 };
            yield return new[] { (object)TestEnum_UInt.uint_3 };
            yield return new[] { (object)TestEnum_Short.short_2 };
            yield return new[] { (object)TestEnum_UShort.ushort_3 };            
            yield return new[] { (object)TestEnum_Long.long_2};
            yield return new[] { (object)TestEnum_ULong.ulong_3 };
            yield return new[] { (object)TestEnum_Byte.byte_4 };
            yield return new[] { (object)TestEnum_SByte.sbyte_2 };
        }
    }
}

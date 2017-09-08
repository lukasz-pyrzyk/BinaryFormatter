using System;
using System.Text;
using BinaryFormatter;
using Xunit;
using System.Collections.Generic;
using System.Numerics;

namespace BinaryFormatterTests
{
    public class BinaryConverterTests
    {
        [Fact]
        public void CanSerialize_Byte()
        {
            var value = byte.MinValue;;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            var deserializedValue = converter.Deserialize<byte>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_SByte()
        {
            sbyte value = sbyte.MinValue;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            sbyte deserializedValue = converter.Deserialize<sbyte>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Char()
        {
            char value = char.MinValue;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            char deserializedValue = converter.Deserialize<char>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Short()
        {
            short value = short.MinValue;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            short deserializedValue = converter.Deserialize<short>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Ushort()
        {
            ushort value = ushort.MinValue;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            ushort deserializedValue = converter.Deserialize<ushort>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_UInt()
        {
            uint value = uint.MinValue;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            uint deserializedValue = converter.Deserialize<uint>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Int()
        {
            int value = int.MinValue;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            int deserializedValue = converter.Deserialize<int>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Ulong()
        {
            ulong value = ulong.MinValue;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            ulong deserializedValue = converter.Deserialize<ulong>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Long()
        {
            long value = long.MinValue;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            long deserializedValue = converter.Deserialize<long>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Float()
        {
            float value = float.MinValue;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            float deserializedValue = converter.Deserialize<float>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Double()
        {
            double value = double.MinValue;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            double deserializedValue = converter.Deserialize<double>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Bool()
        {
            bool value = false;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            bool deserializedValue = converter.Deserialize<bool>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Decimal()
        {
            decimal value = decimal.MinValue;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            decimal deserializedValue = converter.Deserialize<decimal>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_String()
        {
            string value = "lorem ipsum";

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            string deserializedValue = converter.Deserialize<string>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_CyrillicsString()
        {
            string value = "Кто не ходит, тот и не падает.";

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            string deserializedValue = converter.Deserialize<string>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Datetime()
        {
            DateTime value = DateTime.MinValue;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            DateTime deserializedValue = converter.Deserialize<DateTime>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Timespan()
        {
            var value = TimeSpan.MaxValue;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            TimeSpan deserializedValue = converter.Deserialize<TimeSpan>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_DateTimeOffset()
        {
            var value = DateTimeOffset.Now;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            DateTimeOffset deserializedValue = converter.Deserialize<DateTimeOffset>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Null()
        {
            object value = null;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            object deserializedValue = converter.Deserialize<object>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_ByteArray()
        {
            byte[] value = Encoding.UTF8.GetBytes("lorem ipsum");

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            byte[] deserializedValue = converter.Deserialize<byte[]>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_IEnumerable()
        {
            List<string> value = new List<string>();
            value.Add("lorem ipsum");
            value.Add("Кто не ходит, тот и не падает.");

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            List<string> deserializedValue = converter.Deserialize<List<string>>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Guid()
        {
            Guid value = Guid.NewGuid();

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            Guid deserializedValue = converter.Deserialize<Guid>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Uri()
        {
            Uri value = new Uri("https://github.com/lukasz-pyrzyk/BinaryFormatter");

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            Uri deserializedValue = converter.Deserialize<Uri>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_Enum()
        {
            Enum value = DayOfWeek.Sunday;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            Enum deserializedValue = converter.Deserialize<Enum>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_KeyValuePair()
        {
            KeyValuePair<int, string> value = new KeyValuePair<int, string>(1, "one");

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            KeyValuePair<int, string> deserializedValue = converter.Deserialize<KeyValuePair<int, string>>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_BigInteger()
        {
            BigInteger value = BigInteger.Parse("90612345123875509091827560007100099");

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            BigInteger deserializedValue = converter.Deserialize<BigInteger>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_NullableInt_HasValue()
        {
            int? value = 12345678;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            int? deserializedValue = converter.Deserialize<int?>(bytes);

            Assert.Equal(value, deserializedValue);
        }

        [Fact]
        public void CanSerialize_NullableInt_HasNull()
        {
            int? value = null;

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            int? deserializedValue = converter.Deserialize<int?>(bytes);

            Assert.Equal(value, deserializedValue);
        }
    }
}

﻿using System;
using System.Text;
using BinaryFormatter;
using Xunit;

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
        public void CanSerialize_ByteArray()
        {
            byte[] value = Encoding.UTF8.GetBytes("lorem ipsum");

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(value);
            byte[] deserializedValue = converter.Deserialize<byte[]>(bytes);

            Assert.Equal(value, deserializedValue);
        }
    }
}

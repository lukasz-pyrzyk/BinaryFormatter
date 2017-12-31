using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using FluentAssertions;
using Xunit;

namespace BinaryFormatter.Tests
{
    public class BinaryConverterTests
    {
        [Fact]
        public void CanSerialize_Byte()
        {
            var value = byte.MinValue;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_SByte()
        {
            sbyte value = sbyte.MinValue;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Char()
        {
            char value = char.MinValue;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Short()
        {
            short value = short.MinValue;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Ushort()
        {
            ushort value = ushort.MinValue;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_UInt()
        {
            uint value = uint.MinValue;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Int()
        {
            int value = int.MinValue;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Ulong()
        {
            ulong value = ulong.MinValue;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Long()
        {
            long value = long.MinValue;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Float()
        {
            float value = float.MinValue;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Double()
        {
            double value = double.MinValue;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Bool()
        {
            bool value = false;

            bool deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Decimal()
        {
            decimal value = decimal.MinValue;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Theory]
        [InlineData("lorem ipsum")]
        [InlineData("Кто не ходит, тот и не падает.")]
        public void CanSerialize_String(string value)
        {
            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Datetime()
        {
            DateTime value = DateTime.MinValue;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Timespan()
        {
            var value = TimeSpan.MaxValue;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Null()
        {
            object value = null;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().BeNull();
        }

        [Fact]
        public void CanSerialize_ByteArray()
        {
            byte[] value = Encoding.UTF8.GetBytes("lorem ipsum");

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Equal(value);
        }

        [Fact]
        public void CanSerialize_IEnumerable()
        {
            List<string> value = new List<string> { "lorem ipsum", "Кто не ходит, тот и не падает." };

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Equal(value);
        }

        [Fact]
        public void CanSerialize_Guid()
        {
            Guid value = Guid.NewGuid();

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Uri()
        {
            Uri value = new Uri("https://github.com/lukasz-pyrzyk/BinaryFormatter");

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_Enum()
        {
            Enum value = DayOfWeek.Sunday;

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_KeyValuePair()
        {
            KeyValuePair<int, string> value = new KeyValuePair<int, string>(1, "one");

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }

        [Fact]
        public void CanSerialize_BigInteger()
        {
            BigInteger value = BigInteger.Parse("90612345123875509091827560007100099");

            var deserialized = TestHelper.SerializeAndDeserialize(value);

            deserialized.Should().Be(value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using BinaryFormatter.Utils;
using FluentAssertions;
using Xunit;

namespace BinaryFormatter.Tests.Utils
{
    public class TypeHelperTests
    {
        [Fact]
        public void CastFrom_KeyValuePair()
        {
            var kvp = new KeyValuePair<int, string>(1, "one");
            object kvpAsObject = kvp;
            KeyValuePair<object, object> kvpAfterCast = TypeHelper.CastFrom(kvpAsObject);

            kvpAfterCast.Key.Should().Be(kvp.Key);
            kvpAfterCast.Value.Should().Be(kvp.Value);
        }

        [Fact]
        public void IsDictionary_True()
        {
            var valueForCheck = new Dictionary<int, string> { { 1, "one" } };
            TypeHelper.IsDictionary(valueForCheck).Should().BeTrue();
        }

        [Fact]
        public void IsDictionary_False()
        {
            var valueForCheck = "this is not a dictionary";
            TypeHelper.IsDictionary(valueForCheck).Should().BeFalse();
        }

        [Fact]
        public void IsLinkedList_True()
        {
            var valueForCheck = new LinkedList<int>();
            valueForCheck.AddLast(1);
            TypeHelper.IsLinkedList(valueForCheck).Should().BeTrue();
        }

        [Fact]
        public void IsLinkedList_False()
        {
            var valueForCheck = "this is not a linked list";
            TypeHelper.IsLinkedList(valueForCheck).Should().BeFalse();
        }

        [Fact]
        public void HasConversionOperator_True()
        {
            bool hasConversionOperator = TypeHelper.HasConversionOperator(typeof(float), typeof(int));
            hasConversionOperator.Should().BeTrue();
        }

        [Fact]
        public void HasConversionOperator_False()
        {
            bool hasConversionOperator = TypeHelper.HasConversionOperator(typeof(string), typeof(int));
            hasConversionOperator.Should().BeFalse();
        }

        [Theory]
        [InlineData(typeof(byte))]
        [InlineData(typeof(sbyte))]
        [InlineData(typeof(char))]
        [InlineData(typeof(short))]
        [InlineData(typeof(ushort))]
        [InlineData(typeof(int))]
        [InlineData(typeof(uint))]
        [InlineData(typeof(ulong))]
        [InlineData(typeof(long))]
        [InlineData(typeof(float))]
        [InlineData(typeof(double))]
        [InlineData(typeof(decimal))]
        [InlineData(typeof(string))]
        [InlineData(typeof(DateTime))]
        [InlineData(typeof(TimeSpan))]
        [InlineData(typeof(byte[]))]
        [InlineData(typeof(BigInteger))]
        [InlineData(typeof(Guid))]
        [InlineData(typeof(Uri))]
        public void IsSupportedBySerializer(Type type)
        {
            bool supported = type.GetTypeInfo().IsSupportedBySerializer();
            supported.Should().BeTrue();
        }
    }
}

using System.Collections.Generic;
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
        public void IsList_True()
        {
            var valueForCheck = new List<int> { 1 };
            TypeHelper.IsList(valueForCheck).Should().BeTrue();
        }

        [Fact]
        public void IsList_False()
        {
            var valueForCheck = "this is not a list";
            TypeHelper.IsList(valueForCheck).Should().BeFalse();
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
    }
}

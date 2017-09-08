using System;
using System.Text;
using BinaryFormatter.Utils;
using Xunit;
using System.Collections.Generic;
using System.Collections;

namespace BinaryFormatterTests.Utils
{
    public class TypeHelperTests
    {
        [Fact]
        public void CastFrom_KeyValuePair()
        {
            KeyValuePair<int, string> kvp = new KeyValuePair<int, string>(1, "one");
            object kvpAsObject = (object)kvp;
            KeyValuePair<object, object> kvpAfterCast = TypeHelper.CastFrom(kvpAsObject);

            Assert.Equal(kvp.Key, kvpAfterCast.Key);
            Assert.Equal(kvp.Value, kvpAfterCast.Value);
        }

        [Fact]
        public void IsDictionary_True()
        {
            var valueForCheck = new Dictionary<int, string>();
            valueForCheck.Add(1, "one");
            Assert.True(TypeHelper.IsDictionary(valueForCheck));
        }

        [Fact]
        public void IsDictionary_False()
        {
            var valueForCheck = "this is not a dictionary";
            Assert.False(TypeHelper.IsDictionary(valueForCheck));
        }

        [Fact]
        public void IsList_True()
        {
            var valueForCheck = new List<int>();
            valueForCheck.Add(1);
            Assert.True(TypeHelper.IsList(valueForCheck));
        }

        [Fact]
        public void IsList_False()
        {
            var valueForCheck = "this is not a list";
            Assert.False(TypeHelper.IsList(valueForCheck));
        }

        [Fact]
        public void IsLinkedList_True()
        {
            var valueForCheck = new LinkedList<int>();
            valueForCheck.AddLast(1);
            Assert.True(TypeHelper.IsLinkedList(valueForCheck));
        }

        [Fact]
        public void IsLinkedList_False()
        {
            var valueForCheck = "this is not a linked list";
            Assert.False(TypeHelper.IsLinkedList(valueForCheck));
        }

        [Fact]
        public void IsHashSet_True()
        {
            var valueForCheck = new HashSet<int>();
            valueForCheck.Add(1);            
            Assert.True(TypeHelper.IsHashSet(valueForCheck));
        }

        [Fact]
        public void IsHashSet_False()
        {
            var valueForCheck = "this is not a linked list";
            Assert.False(TypeHelper.IsHashSet(valueForCheck));
        }

        [Fact]
        public void HasConversionOperator_True()
        {
            bool hasConversionOperator = TypeHelper.HasConversionOperator(typeof(float), typeof(int));
            Assert.True(hasConversionOperator);
        }

        [Fact]
        public void HasConversionOperator_False()
        {
            bool hasConversionOperator = TypeHelper.HasConversionOperator(typeof(string), typeof(int));
            Assert.False(hasConversionOperator);
        }

        [Fact]
        public void GetCollectionCount_IEnumerable()
        {
            IEnumerable<int> collection = new List<int>();
            ((IList)collection).Add(1);
            ((IList)collection).Add(2);
            ((IList)collection).Add(3);

            Assert.Equal(3, TypeHelper.GetCollectionCount(collection));
        }

        [Fact]
        public void GetCollectionCount_HashSet()
        {
            HashSet<int> collection = new HashSet<int>();
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);
            collection.Add(4);
            collection.Add(5);

            Assert.Equal(5, TypeHelper.GetCollectionCount(collection));
        }
    }
}

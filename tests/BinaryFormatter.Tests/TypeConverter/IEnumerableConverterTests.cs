using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class IEnumerableConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize_SimpleCollection()
        {
            var converter = new BinaryConverter();

            List<object> simpleCollection = new List<object>();
            simpleCollection.Add(true);
            simpleCollection.Add(Encoding.UTF8.GetBytes("lorem ipsum"));
            simpleCollection.Add(byte.MaxValue);
            simpleCollection.Add(char.MaxValue);
            simpleCollection.Add(DateTime.Now);
            simpleCollection.Add(decimal.MaxValue);
            simpleCollection.Add(double.MaxValue);
            simpleCollection.Add(float.MaxValue);
            simpleCollection.Add(int.MaxValue);
            simpleCollection.Add(long.MaxValue);
            simpleCollection.Add(sbyte.MaxValue);
            simpleCollection.Add(short.MaxValue);
            simpleCollection.Add("Lorem ipsum");
            simpleCollection.Add("Кто не ходит, тот и не падает.");
            simpleCollection.Add(uint.MaxValue);
            simpleCollection.Add(ulong.MaxValue);
            simpleCollection.Add(ushort.MaxValue);
            simpleCollection.Add(Guid.NewGuid());
            simpleCollection.Add(new KeyValuePair<int, string>(5, "five"));

            byte[] bytesSimpleCollection = converter.Serialize(simpleCollection);
            var valueFromBytesSimpleCollection = converter.Deserialize<List<object>>(bytesSimpleCollection);
            Assert.Equal(valueFromBytesSimpleCollection, simpleCollection);
        }

        [Fact]
        public void CanSerializeAndDeserialize_ComplexCollection()
        {
            var converter = new BinaryConverter();

            List<object> complexCollection = new List<object>();
            complexCollection.Add(new WithTestProperties()
            {
                Name = "John",
                Age = 60,
                Birthday = DateTime.Now.AddYears(-60),
                Friends = new List<string>()
                {
                    "Joe",
                    "Linus",
                    "Bill",
                    "Andrew"
                }
            });
            complexCollection.Add(
                new List<string>()
                {
                    "lorem ipsum",
                    "Кто не ходит, тот и не падает."
                }
            );
            complexCollection.Add(new KeyValuePair<int, string>(1, "one"));
            complexCollection.Add(new KeyValuePair<int, string>(2, "two"));
            complexCollection.Add(new KeyValuePair<int, string>(3, "three"));
            byte[] bytesComplexCollection = converter.Serialize(complexCollection);
            List<object> valueFromBytesComplexCollection = converter.Deserialize<List<object>>(bytesComplexCollection);

            var objectFromCollection_before = (WithTestProperties)complexCollection[0];
            var objectFromCollection_after = (WithTestProperties)valueFromBytesComplexCollection[0];
            Assert.Equal(objectFromCollection_before.Name, objectFromCollection_after.Name);
            Assert.Equal(objectFromCollection_before.Age, objectFromCollection_after.Age);
            Assert.Equal(objectFromCollection_before.Birthday, objectFromCollection_after.Birthday);

            var listFromCollection_before = complexCollection[1] as IEnumerable;
            var listFromCollection_after = valueFromBytesComplexCollection[1] as IEnumerable;
            Assert.Equal(listFromCollection_before, listFromCollection_after);

            Assert.Equal(complexCollection[2], valueFromBytesComplexCollection[2]);
            Assert.Equal(complexCollection[3], valueFromBytesComplexCollection[3]);
            Assert.Equal(complexCollection[4], valueFromBytesComplexCollection[4]);
        }

        [Fact]
        public void CanSerializeAndDeserialize_ReadOnlyCollection()
        {
            var converter = new BinaryConverter();

            IReadOnlyCollection<int> simpleCollection = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            
            byte[] bytesSimpleCollection = converter.Serialize(simpleCollection);
            var valueFromBytesSimpleCollection = converter.Deserialize<IReadOnlyCollection<int>>(bytesSimpleCollection);
            Assert.Equal(valueFromBytesSimpleCollection, simpleCollection);
        }

        [Fact]
        public void CanSerializeAndDeserialize_SimpleIDictionaryCollection()
        {
            var converter = new BinaryConverter();

            Dictionary<int, string> dictionaryCollection = new Dictionary<int, string>();
            dictionaryCollection.Add(0, "zero");
            dictionaryCollection.Add(1, "one");
            dictionaryCollection.Add(2, "two");
            dictionaryCollection.Add(3, "three");
            dictionaryCollection.Add(4, "four");
            dictionaryCollection.Add(5, "five");
            dictionaryCollection.Add(6, "six");
            dictionaryCollection.Add(7, "seven");
            dictionaryCollection.Add(8, "eight");
            dictionaryCollection.Add(9, "nine");            

            byte[] bytesDictionaryCollection = converter.Serialize(dictionaryCollection);
            var valueFromBytesDictionaryCollection = converter.Deserialize<Dictionary<int, string>>(bytesDictionaryCollection);
            Assert.Equal(valueFromBytesDictionaryCollection, dictionaryCollection);
        }

        [Fact]
        public void CanSerializeAndDeserialize_ComplexIDictionaryCollection()
        {
            var converter = new BinaryConverter();

            Dictionary<int, object> dictionaryCollection = new Dictionary<int, object>();
            dictionaryCollection.Add(1, new WithTestProperties() {
                Name = "Joe",
                Age = 30,
                Birthday = DateTime.Now.AddYears(-30),
                Friends = new List<string>()
                {
                    "Jim",
                    "John",
                    "Linus"
                }
            });
            var dictionaryForTest = new Dictionary<int, int>();
            dictionaryForTest.Add(1, 1);
            dictionaryForTest.Add(2, 2);
            dictionaryForTest.Add(3, 3);
            dictionaryCollection.Add(2, dictionaryForTest);

            byte[] bytesDictionaryCollection = converter.Serialize(dictionaryCollection);
            var valueFromBytesDictionaryCollection = converter.Deserialize<Dictionary<int, object>>(bytesDictionaryCollection);

            // Check first element
            object valBefore;
            dictionaryCollection.TryGetValue(1, out valBefore);
            object valAfter;
            valueFromBytesDictionaryCollection.TryGetValue(1, out valAfter);

            Assert.Equal(((WithTestProperties)valBefore).Name, ((WithTestProperties)valAfter).Name);
            Assert.Equal(((WithTestProperties)valBefore).Age, ((WithTestProperties)valAfter).Age);
            Assert.Equal(((WithTestProperties)valBefore).Birthday, ((WithTestProperties)valAfter).Birthday);
            Assert.Equal(((WithTestProperties)valBefore).Friends, ((WithTestProperties)valAfter).Friends);

            // Check second element
            object valDictionaryBefore;
            dictionaryCollection.TryGetValue(2, out valDictionaryBefore);
            object valDictionaryAfter;
            valueFromBytesDictionaryCollection.TryGetValue(2, out valDictionaryAfter);

            Assert.Equal(valDictionaryBefore, valDictionaryAfter);
        }

        [Fact]
        public void CanSerializeAndDeserialize_SimpleLinkedListCollection()
        {
            var converter = new BinaryConverter();

            LinkedList<string> linkedListCollection = new LinkedList<string>();
            linkedListCollection.AddLast("zero");
            linkedListCollection.AddLast("one");
            linkedListCollection.AddLast("two");
            linkedListCollection.AddLast("three");
            linkedListCollection.AddLast("four");
            linkedListCollection.AddLast("five");
            linkedListCollection.AddLast("six");
            linkedListCollection.AddLast("seven");
            linkedListCollection.AddLast("eight");
            linkedListCollection.AddLast("nine");

            byte[] bytesLinkedListCollection = converter.Serialize(linkedListCollection);
            var valueFromBytesLinkedListCollection = converter.Deserialize<LinkedList<string>>(bytesLinkedListCollection);
            Assert.Equal(valueFromBytesLinkedListCollection, linkedListCollection);
        }

        [Fact]
        public void CanSerializeAndDeserialize_ComplexLinkedListCollection()
        {
            var converter = new BinaryConverter();

            LinkedList<object> linkedListCollection = new LinkedList<object>();
            linkedListCollection.AddLast("zero");
            linkedListCollection.AddLast(1);
            linkedListCollection.AddLast(new List<string>() { "I", "love", "the", ".NET Core" });

            byte[] bytesLinkedListCollection = converter.Serialize(linkedListCollection);
            var valueFromBytesLinkedListCollection = converter.Deserialize<LinkedList<object>>(bytesLinkedListCollection);
            Assert.Equal(valueFromBytesLinkedListCollection, linkedListCollection);
        }

        class WithTestProperties
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public DateTime Birthday { get; set; }
            public List<string> Friends { get; set; }            
        }
    }
}

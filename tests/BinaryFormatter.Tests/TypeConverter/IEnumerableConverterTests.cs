using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class EnumerableConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize_SimpleCollection()
        {
            var before = new List<object>
            {
                true,
                Encoding.UTF8.GetBytes("lorem ipsum"),
                byte.MaxValue,
                char.MaxValue,
                DateTime.Now,
                decimal.MaxValue,
                double.MaxValue,
                float.MaxValue,
                int.MaxValue,
                long.MaxValue,
                sbyte.MaxValue,
                short.MaxValue,
                "Lorem ipsum",
                "Кто не ходит, тот и не падает.",
                uint.MaxValue,
                ulong.MaxValue,
                ushort.MaxValue,
                Guid.NewGuid(),
                new KeyValuePair<int, string>(5, "five")
            };

            var after = TestHelper.SerializeAndDeserialize(before);
            Assert.Equal(after, before);
        }

        [Fact]
        public void CanSerializeAndDeserialize_ComplexCollection()
        {
            var before = new List<object>
            {
                new WithTestProperties
                {
                    Name = "John",
                    Age = 60,
                    Birthday = DateTime.Now.AddYears(-60),
                    Friends = new List<string>
                    {
                        "Joe",
                        "Linus",
                        "Bill",
                        "Andrew"
                    }
                }
            };
            before.Add(
                new List<string>
                {
                    "lorem ipsum",
                    "Кто не ходит, тот и не падает."
                }
            );
            before.Add(new KeyValuePair<int, string>(1, "one"));
            before.Add(new KeyValuePair<int, string>(2, "two"));
            before.Add(new KeyValuePair<int, string>(3, "three"));

            List<object> after = TestHelper.SerializeAndDeserialize(before);

            var objectFromCollectionBefore = (WithTestProperties)before[0];
            var objectFromCollectionAfter = (WithTestProperties)after[0];
            Assert.Equal(objectFromCollectionBefore.Name, objectFromCollectionAfter.Name);
            Assert.Equal(objectFromCollectionBefore.Age, objectFromCollectionAfter.Age);
            Assert.Equal(objectFromCollectionBefore.Birthday, objectFromCollectionAfter.Birthday);

            var listFromCollectionBefore = before[1] as IEnumerable;
            var listFromCollectionAfter = after[1] as IEnumerable;
            Assert.Equal(listFromCollectionBefore, listFromCollectionAfter);

            Assert.Equal(before[2], after[2]);
            Assert.Equal(before[3], after[3]);
            Assert.Equal(before[4], after[4]);
        }

        [Fact]
        public void CanSerializeAndDeserialize_ReadOnlyCollection()
        {
            var converter = new BinaryConverter();

            IReadOnlyCollection<int> simpleCollection = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            byte[] bytesSimpleCollection = converter.Serialize(simpleCollection);
            var valueFromBytesSimpleCollection = converter.Deserialize<IReadOnlyCollection<int>>(bytesSimpleCollection);
            Assert.Equal(valueFromBytesSimpleCollection, simpleCollection);
        }

        [Fact]
        public void CanSerializeAndDeserialize_SimpleIDictionaryCollection()
        {
            var before = new Dictionary<int, string>
            {
                {0, "zero"},
                {1, "one"},
                {2, "two"},
                {3, "three"},
                {4, "four"},
                {5, "five"},
                {6, "six"},
                {7, "seven"},
                {8, "eight"},
                {9, "nine"}
            };

            var after = TestHelper.SerializeAndDeserialize(before);
            Assert.Equal(after, before);
        }

        [Fact]
        public void CanSerializeAndDeserialize_ComplexIDictionaryCollection()
        {
            var converter = new BinaryConverter();
            Dictionary<int, object> dictionaryCollection = new Dictionary<int, object>();
            dictionaryCollection.Add(1, new WithTestProperties
            {
                Name = "Joe",
                Age = 30,
                Birthday = DateTime.Now.AddYears(-30),
                Friends = new List<string>
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
            linkedListCollection.AddLast(new List<string> { "I", "love", "the", ".NET Core" });

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

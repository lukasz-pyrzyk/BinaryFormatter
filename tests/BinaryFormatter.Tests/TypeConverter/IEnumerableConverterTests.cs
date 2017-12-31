using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
// ReSharper disable UnusedAutoPropertyAccessor.Local

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
                },
                new List<string>
                {
                    "lorem ipsum",
                    "Кто не ходит, тот и не падает."
                },
                new KeyValuePair<int, string>(1, "one"),
                new KeyValuePair<int, string>(2, "two"),
                new KeyValuePair<int, string>(3, "three")
            };

            var after = TestHelper.SerializeAndDeserialize(before);

            after[0].Should().BeEquivalentTo(before[0]);

            var collectionBefore = before[1] as IEnumerable;
            var collectionAfter = after[1] as IEnumerable;
            collectionAfter.Should().Equal(collectionBefore);

            before[2].Should().Be(after[2]);
            before[3].Should().Be(after[3]);
            before[4].Should().Be(after[4]);
        }

        [Fact]
        public void CanSerializeAndDeserialize_ReadOnlyCollection()
        {
            IReadOnlyCollection<int> before = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var after = TestHelper.SerializeAndDeserialize(before);
            after.Should().Equal(before);
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
            var before = new Dictionary<int, object>
            {
                {
                    1, new WithTestProperties
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
                    }
                }
            };
            var dictionaryForTest = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } };
            before.Add(2, dictionaryForTest);

            var after = TestHelper.SerializeAndDeserialize(before);

            // Check first element
            before.TryGetValue(1, out var valBefore);
            after.TryGetValue(1, out var valAfter);

            ((WithTestProperties) valBefore).Should().BeEquivalentTo((WithTestProperties) valAfter);

            // Check second element
            before.TryGetValue(2, out var valDictionaryBefore);
            after.TryGetValue(2, out object valDictionaryAfter);

            Assert.Equal(valDictionaryBefore, valDictionaryAfter);
        }

        [Fact]
        public void CanSerializeAndDeserialize_SimpleLinkedListCollection()
        {
            var before = new LinkedList<string>();
            before.AddLast("zero");
            before.AddLast("one");
            before.AddLast("two");
            before.AddLast("three");
            before.AddLast("four");
            before.AddLast("five");
            before.AddLast("six");
            before.AddLast("seven");
            before.AddLast("eight");
            before.AddLast("nine");

            var after = TestHelper.SerializeAndDeserialize(before);
            after.Should().Equal(before);
        }

        [Fact]
        public void CanSerializeAndDeserialize_ComplexLinkedListCollection()
        {
            var before = new LinkedList<object>();
            before.AddLast("zero");
            before.AddLast(1);
            before.AddLast(new List<string> { "I", "love", "the", ".NET Core" });

            var after = TestHelper.SerializeAndDeserialize(before);
            Assert.Equal(after, before);
        }

        private class WithTestProperties
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public DateTime Birthday { get; set; }
            public List<string> Friends { get; set; }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
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

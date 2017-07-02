using BinaryFormatter.TypeConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class IEnumerableConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            IEnumerableConverter converter = new IEnumerableConverter();

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

            byte[] bytesSimpleCollection = converter.Serialize(simpleCollection);
            var valueFromBytesSimpleCollection = converter.Deserialize(bytesSimpleCollection);
            Assert.Equal(valueFromBytesSimpleCollection, simpleCollection);

            List<object> complexCollection = new List<object>();
            complexCollection.Add(new WithTestProperties()
            {
                Name = "John",
                Age = 60,
                Birthday = DateTime.Now.AddYears(-60)
            });
            complexCollection.Add(
                new List<string>()
                {
                    "lorem ipsum",
                    "Кто не ходит, тот и не падает."
                }    
            );
            byte[] bytesComplexCollection = converter.Serialize(complexCollection);
            var valueFromBytesComplexCollection = converter.Deserialize(bytesComplexCollection);
        }

        class WithTestProperties
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public DateTime Birthday { get; set; }
        }
    }
}

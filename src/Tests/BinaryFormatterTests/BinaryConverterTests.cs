using System;
using System.Text;
using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests
{
    public class BinaryConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            Test obj = new Test(5, 4, "Lorem ipsum");

            BinaryConverter p = new BinaryConverter();
            byte[] array = p.Serialize(obj);
            Test objFromBytes = p.Deserialize<Test>(array);
            
            Assert.Equal(obj.X, objFromBytes.X);
            Assert.Equal(obj.Y, objFromBytes.Y);
            Assert.Equal(obj.Word, objFromBytes.Word);
            Assert.Equal(obj.WordBytes, objFromBytes.WordBytes);
        }

        internal class Test
        {
            public Test(int x, int y, string word)
            {
                X = x;
                Y = y;
                Word = word;
                WordBytes = Encoding.UTF8.GetBytes(word);
            }

            public Test()
            {
            }

            public int X { get; set; }
            public string Word { get; set; }
            public int Y { get; set; }
            public byte[] WordBytes { get; set; }
        }
    }
}

using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests
{
    public class ParserTests
    {
        [Fact]
        public void DoWork()
        {
            Test test = new Test(5, 4, "jedynascie");

            BinaryConverter p = new BinaryConverter();
            byte[] array = p.Parse(test);
        }

        internal class Test
        {
            public Test(int x, int y, string word)
            {
                X = x;
                Y = y;
                Word = word;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public string Word { get; set; }
        }
    }
}

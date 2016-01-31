using System;
using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests
{
    public class BinaryConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserializeNestedTypes()
        {
            Test obj = new Test
            {
                Two = new TestTwo
                {
                    Message = "lorem",
                    Three = new TestThree
                    {
                        Date = DateTime.Now,
                        Z = 5
                    }
                },
                X = 1
            };

            BinaryConverter p = new BinaryConverter();
            byte[] array = p.Serialize(obj);
            Test objFromBytes = p.Deserialize<Test>(array);

            Assert.Equal(obj.X, objFromBytes.X);
            Assert.Equal(obj.Two.Message, objFromBytes.Two.Message);
            Assert.Equal(obj.Two.Three.Date, objFromBytes.Two.Three.Date);
            Assert.Equal(obj.Two.Three.Z, objFromBytes.Two.Three.Z);
        }

        internal class Test
        {
            public int X { get; set; }
            public TestTwo Two { get; set; }
        }

        internal class TestTwo
        {
            public string Message { get; set; }
            public TestThree Three { get; set; }
        }

        internal class TestThree
        {
            public DateTime Date { get; set; }
            public int Z { get; set; }
        }
    }
}

using System;
using System.Text;
using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests
{
    public class BinaryConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserializeNestedTypes()
        {
            Test model = new Test
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

            BinaryConverter converter = new BinaryConverter();
            byte[] array = converter.Serialize(model);
            Test objFromBytes = converter.Deserialize<Test>(array);

            Assert.Equal(objFromBytes.X, model.X);
            Assert.Equal(objFromBytes.Two.Message, model.Two.Message);
            Assert.Equal(objFromBytes.Two.Three.Date, model.Two.Three.Date);
            Assert.Equal(objFromBytes.Two.Three.Z, model.Two.Three.Z);
        }

        [Fact]
        public void CanSerializeAndDeserializeNestedTypeWithByteArray()
        {
            TestFour model = new TestFour
            {
                ByteArray = new ByteArray
                {
                    Array = Encoding.UTF8.GetBytes("lorem ipsum")
                },
                X = 15
            };

            BinaryConverter converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(model);
            TestFour modelFromBytes = converter.Deserialize<TestFour>(bytes);

            Assert.Equal(modelFromBytes.X, model.X);
            Assert.Equal(modelFromBytes.ByteArray.Array, model.ByteArray.Array);
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

        internal class TestFour
        {
            public int X { get; set; }
            public ByteArray ByteArray { get; set; }
        }

        internal class ByteArray
        {
            public byte[] Array { get; set; }
        }
    }
}

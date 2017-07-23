using System;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class EnumConverterTests : ConverterTest<Enum>
    {
        public override Enum Value => TestEnum.test2;

        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }

        public enum TestEnum : int
        {
            test0 = 0,
            test1 = 1,
            test2 = 2,
            test3 = 3,
            test4 = 4,
            test5 = 5
        }
    }
}

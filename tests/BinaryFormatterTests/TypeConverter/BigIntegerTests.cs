using System;
using System.Numerics;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class BigIntegerTests : ConverterTest<BigInteger>
    {
        public override BigInteger Value => BigInteger.Parse("90612345123875509091827560007100099");

        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }
    }
}

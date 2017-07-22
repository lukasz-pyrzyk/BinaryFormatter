using System;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class GuidConverterTests : ConverterTest<Guid>
    {
        public override Guid Value => Guid.Parse("af26f890-9202-46f2-ac31-f5e7abc098a3");

        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }
    }
}

using System.Collections.Generic;
using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class KeyValuePairConverterTests : ConverterTest<KeyValuePair<int, string>>
    {
        public override KeyValuePair<int, string> Value => new KeyValuePair<int, string>(10, "ten");

        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }
    }
}

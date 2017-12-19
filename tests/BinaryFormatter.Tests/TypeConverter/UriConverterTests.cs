using System;
using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class UriConverterTests : ConverterTest<Uri>
    {
        public override Uri Value => new Uri("https://github.com/lukasz-pyrzyk/BinaryFormatter");

        [Fact]
        public void CanSerializeAndDeserialize()
        {
            RunTest();
        }
    }
}

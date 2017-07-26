using System;
using System.Text;
using BinaryFormatter.Utils;
using Xunit;

namespace BinaryFormatterTests.Utils
{
    public class TypeExtensionsTests
    {
        [Theory]
        [InlineData("System.String", typeof(string))]
        public void CanResolveTypeWithUTF8(string typeName, Type expectedType)
        {
            byte[] data = Encoding.UTF8.GetBytes(typeName);

            Type type = TypeUtils.FromUTF8Bytes(data);

            Assert.Equal(expectedType, type);
        }
    }
}

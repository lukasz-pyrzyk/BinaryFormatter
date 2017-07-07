using BinaryFormatter.Utils;
using System.IO;
using Xunit;

namespace BinaryFormatterTests.Utils
{
    public class StreamExtensionsTests
    {
        [Fact]
        public void Write_WritesAllElementsToTheStream()
        {
            var stream = new MemoryStream();
            byte[] data = new byte[10000];
            stream.Write(data);

            byte[] dataFromStream = stream.ToArray();

            Assert.Equal(data, dataFromStream);
        }
    }
}

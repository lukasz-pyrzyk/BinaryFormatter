using System.IO;

namespace BinaryFormatter.Utils
{
    public static class StreamExtensions
    {
        public static void Write(this Stream stream, byte[] buffer)
        {
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}

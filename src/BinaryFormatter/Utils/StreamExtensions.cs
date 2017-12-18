using System;
using System.IO;

namespace BinaryFormatter.Utils
{
    public static class StreamExtensions
    {
        public static void Write(this Stream stream, params byte[] buffer)
        {
            stream.Write(buffer, 0, buffer.Length);
        }
        
        public static void WriteWithLengthPrefix(this Stream stream, byte[] buffer)
        {
            byte[] sizePrefix = BitConverter.GetBytes(buffer.Length);
            stream.Write(sizePrefix);
            stream.Write(buffer);
        }
    }
}

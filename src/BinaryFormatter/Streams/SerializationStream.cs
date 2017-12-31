using System;
using System.IO;

namespace BinaryFormatter.Streams
{
    internal class SerializationStream
    {
        private readonly Stream _internalStream;

        public SerializationStream(Stream internalStream)
        {
            this._internalStream = internalStream;
        }

        public void Write(params byte[] buffer)
        {
            _internalStream.Write(buffer, 0, buffer.Length);
        }

        public void WriteWithLengthPrefix(byte[] buffer)
        {
            byte[] sizePrefix = BitConverter.GetBytes(buffer.Length);

            Write(sizePrefix);
            Write(buffer);
        }
    }
}

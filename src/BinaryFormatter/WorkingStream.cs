using System;
using BinaryFormatter.Types;

namespace BinaryFormatter
{
    internal class WorkingStream
    {
        /// <summary>
        /// TODO: remote this property and all usages
        /// </summary>
        public byte[] Stream => stream;

        private readonly byte[] stream;
        private int offset;

        public WorkingStream(byte[] stream)
        {
            this.stream = stream;
        }

        public void CopyInto(byte[] bytes)
        {
            Array.Copy(stream, offset, bytes, 0, bytes.Length);
            offset += bytes.Length;
        }

        public SerializedType ReadSerializedType()
        {
            short type = BitConverter.ToInt16(stream, offset);
            offset += sizeof(short);
            return (SerializedType)type;
        }

        public int ReadInt()
        {
            int value = BitConverter.ToInt32(stream, offset);
            offset += sizeof(int);
            return value;
        }

        public byte[] ReadBytesWithSizePrefix()
        {
            int size = ReadInt();
            var newArray = new byte[size];
            Array.Copy(stream, offset, newArray, 0, newArray.Length);
            offset += newArray.Length;

            return newArray;
        }
    }
}

using System;
using System.Text;
using BinaryFormatter.Types;

namespace BinaryFormatter
{
    internal class WorkingStream
    {
        public bool HasEnded => offset == stream.Length;

        public int Offset => offset;

        private readonly byte[] stream;
        private int offset;

        public WorkingStream(byte[] stream)
        {
            this.stream = stream;
        }

        public void AddOffset(int count) => offset += count;
        public void ChangeOffset(int position) => offset = position;

        public bool ReadBool()
        {
            var value = BitConverter.ToBoolean(stream, offset);
            offset += sizeof(bool);
            return value;
        }

        public byte ReadByte()
        {
            var value = (byte)BitConverter.ToUInt16(stream, offset);
            offset += sizeof(byte);
            return value;
        }

        public char ReadChar()
        {
            var value = BitConverter.ToChar(stream, offset);
            offset += sizeof(char);
            return value;
        }

        public short ReadShort()
        {
            var value = BitConverter.ToInt16(stream, offset);
            offset += sizeof(short);
            return value;
        }

        public ushort ReadUShort()
        {
            var value =  BitConverter.ToUInt16(stream, offset);
            offset += sizeof(ushort);
            return value;
        }

        public int ReadInt()
        {
            int value = BitConverter.ToInt32(stream, offset);
            offset += sizeof(int);
            return value;
        }

        public uint ReadUInt()
        {
            var value = BitConverter.ToUInt32(stream, offset);
            offset += sizeof(uint);
            return value;
        }

        public float ReadFloat()
        {
            var value = BitConverter.ToSingle(stream, offset);
            offset += sizeof(float);
            return value;
        }

        public double ReadDouble()
        {
            var value = BitConverter.ToDouble(stream, offset);
            offset += sizeof(double);
            return value;
        }

        public long ReadLong()
        {
            var value = BitConverter.ToInt64(stream, offset);
            offset += sizeof(long);
            return value;
        }

        public ulong ReadULong()
        {
            var value = BitConverter.ToUInt64(stream, offset);
            offset += sizeof(ulong);
            return value;
        }

        public byte[] ReadBytes(int count)
        {
            var newArray = new byte[count];
            Array.Copy(stream, offset, newArray, 0, newArray.Length);
            offset += newArray.Length;
            return newArray;
        }

        public byte[] ReadBytesWithSizePrefix()
        {
            int size = ReadInt();
            
            return ReadBytes(size);
        }

        public string ReadUTF8WithSizePrefix()
        {
            int size = ReadInt();
            return Encoding.UTF8.GetString(stream, offset, size);
        }

        public SerializedType ReadSerializedType()
        {
            short type = BitConverter.ToInt16(stream, offset);
            offset += sizeof(SerializedType);
            return (SerializedType)type;
        }
    }
}

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

        public WorkingStream(byte[] stream, int position) : this(stream)
        {
            SetOffset(position);
        }

        public void SetOffset(int position) => offset = position;

        public bool ReadBool()
        {
            var value = BitConverter.ToBoolean(stream, offset);
            offset += sizeof(bool);
            return value;
        }

        public byte ReadByte()
        {
            return stream[offset++];
        }

        public sbyte ReadSByte()
        {
            return (sbyte)ReadByte();
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
            var value = BitConverter.ToUInt16(stream, offset);
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
            uint value = BitConverter.ToUInt32(stream, offset);
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
            byte[] data = ReadBytesWithSizePrefix();
            return Encoding.UTF8.GetString(data, 0, data.Length);
        }

        public Type ReadType()
        {
            string typeFullName = ReadUTF8WithSizePrefix();
            return Type.GetType(typeFullName);
        }

        public SerializedType ReadSerializedType()
        {
            short type = BitConverter.ToInt16(stream, offset);
            offset += sizeof(SerializedType);
            return (SerializedType)type;
        }

        public decimal ReadDecimal()
        {
            var bits = new int[4];
            for (int i = 0; i < bits.Length; i++)
            {
                bits[i] = ReadInt();
            }

            return new decimal(bits);
        }
    }
}

using System;
using System.Text;
using BinaryFormatter.Types;

namespace BinaryFormatter.Streams
{
    internal class DeserializationStream
    {
        public bool HasEnded => Offset == _stream.Length;

        public int Offset { get; private set; }

        private readonly byte[] _stream;

        public DeserializationStream(byte[] stream)
        {
            _stream = stream;
        }

        public DeserializationStream(byte[] stream, int position) : this(stream)
        {
            SetOffset(position);
        }

        public void SetOffset(int position) => Offset = position;

        public bool ReadBool()
        {
            var value = BitConverter.ToBoolean(_stream, Offset);
            Offset += sizeof(bool);
            return value;
        }

        public byte ReadByte()
        {
            return _stream[Offset++];
        }

        public sbyte ReadSByte()
        {
            return (sbyte)ReadByte();
        }

        public char ReadChar()
        {
            var value = BitConverter.ToChar(_stream, Offset);
            Offset += sizeof(char);
            return value;
        }

        public short ReadShort()
        {
            var value = BitConverter.ToInt16(_stream, Offset);
            Offset += sizeof(short);
            return value;
        }

        public ushort ReadUShort()
        {
            var value = BitConverter.ToUInt16(_stream, Offset);
            Offset += sizeof(ushort);
            return value;
        }

        public int ReadInt()
        {
            int value = BitConverter.ToInt32(_stream, Offset);
            Offset += sizeof(int);
            return value;
        }

        public uint ReadUInt()
        {
            uint value = BitConverter.ToUInt32(_stream, Offset);
            Offset += sizeof(uint);
            return value;
        }

        public float ReadFloat()
        {
            var value = BitConverter.ToSingle(_stream, Offset);
            Offset += sizeof(float);
            return value;
        }

        public double ReadDouble()
        {
            var value = BitConverter.ToDouble(_stream, Offset);
            Offset += sizeof(double);
            return value;
        }

        public long ReadLong()
        {
            var value = BitConverter.ToInt64(_stream, Offset);
            Offset += sizeof(long);
            return value;
        }

        public ulong ReadULong()
        {
            var value = BitConverter.ToUInt64(_stream, Offset);
            Offset += sizeof(ulong);
            return value;
        }

        public byte[] ReadBytes(int count)
        {
            var newArray = new byte[count];
            Array.Copy(_stream, Offset, newArray, 0, newArray.Length);
            Offset += newArray.Length;
            return newArray;
        }

        public byte[] ReadBytesWithSizePrefix()
        {
            int size = ReadInt();

            return ReadBytes(size);
        }

        public string ReadUtf8WithSizePrefix()
        {
            byte[] data = ReadBytesWithSizePrefix();
            return Encoding.UTF8.GetString(data, 0, data.Length);
        }

        public Type ReadType()
        {
            string typeFullName = ReadUtf8WithSizePrefix();
            return Type.GetType(typeFullName);
        }

        public SerializedType ReadSerializedType()
        {
            short type = BitConverter.ToInt16(_stream, Offset);
            Offset += sizeof(SerializedType);
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

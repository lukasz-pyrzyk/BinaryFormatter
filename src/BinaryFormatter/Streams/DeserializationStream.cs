using System;
using System.Text;
using BinaryFormatter.Types;

namespace BinaryFormatter.Streams
{
    internal class DeserializationStream
    {
        public bool HasEnded => _offset == _stream.Length;

        public int Offset => _offset;

        private readonly byte[] _stream;
        private int _offset;

        public DeserializationStream(byte[] stream)
        {
            _stream = stream;
        }

        public DeserializationStream(byte[] stream, int position) : this(stream)
        {
            SetOffset(position);
        }

        public void SetOffset(int position) => _offset = position;

        public bool ReadBool()
        {
            var value = BitConverter.ToBoolean(_stream, _offset);
            _offset += sizeof(bool);
            return value;
        }

        public byte ReadByte()
        {
            return _stream[_offset++];
        }

        public sbyte ReadSByte()
        {
            return (sbyte)ReadByte();
        }

        public char ReadChar()
        {
            var value = BitConverter.ToChar(_stream, _offset);
            _offset += sizeof(char);
            return value;
        }

        public short ReadShort()
        {
            var value = BitConverter.ToInt16(_stream, _offset);
            _offset += sizeof(short);
            return value;
        }

        public ushort ReadUShort()
        {
            var value = BitConverter.ToUInt16(_stream, _offset);
            _offset += sizeof(ushort);
            return value;
        }

        public int ReadInt()
        {
            int value = BitConverter.ToInt32(_stream, _offset);
            _offset += sizeof(int);
            return value;
        }

        public uint ReadUInt()
        {
            uint value = BitConverter.ToUInt32(_stream, _offset);
            _offset += sizeof(uint);
            return value;
        }

        public float ReadFloat()
        {
            var value = BitConverter.ToSingle(_stream, _offset);
            _offset += sizeof(float);
            return value;
        }

        public double ReadDouble()
        {
            var value = BitConverter.ToDouble(_stream, _offset);
            _offset += sizeof(double);
            return value;
        }

        public long ReadLong()
        {
            var value = BitConverter.ToInt64(_stream, _offset);
            _offset += sizeof(long);
            return value;
        }

        public ulong ReadULong()
        {
            var value = BitConverter.ToUInt64(_stream, _offset);
            _offset += sizeof(ulong);
            return value;
        }

        public byte[] ReadBytes(int count)
        {
            var newArray = new byte[count];
            Array.Copy(_stream, _offset, newArray, 0, newArray.Length);
            _offset += newArray.Length;
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
            short type = BitConverter.ToInt16(_stream, _offset);
            _offset += sizeof(SerializedType);
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

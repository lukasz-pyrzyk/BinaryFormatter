using System;
using System.IO;
using System.Numerics;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class BigIntegerConverter : BaseTypeConverter<BigInteger>
    {
        private int Size { get; set; }

        protected override void WriteObjectToStream(BigInteger obj, Stream stream)
        {
            byte[] data = obj.ToByteArray();
            stream.WriteWithLengthPrefix(data);

            Size = data.Length;
        }

        protected override BigInteger ProcessDeserialize(byte[] bytes, Type sourceType, ref int offset)
        {
            int dataSize = BitConverter.ToInt32(bytes, offset);
            offset += sizeof(int);

            byte[] bigIntegerData = new byte[dataSize];
            Array.Copy(bytes, offset, bigIntegerData, 0, dataSize);

            return new BigInteger(bigIntegerData);
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.BitInteger;
    }
}

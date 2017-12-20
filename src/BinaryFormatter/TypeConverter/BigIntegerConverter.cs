using System;
using System.IO;
using System.Numerics;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class BigIntegerConverter : BaseTypeConverter<BigInteger>
    {
        protected override void SerializeInternal(BigInteger obj, Stream stream)
        {
            byte[] data = obj.ToByteArray();
            stream.WriteWithLengthPrefix(data);
        }

        protected override BigInteger DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            byte[] bigIntegerData = stream.ReadBytesWithSizePrefix();

            return new BigInteger(bigIntegerData);
        }

        public override SerializedType Type => SerializedType.BitInteger;
    }
}

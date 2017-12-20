using System;
using System.Numerics;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class BigIntegerConverter : BaseTypeConverter<BigInteger>
    {
        protected override void SerializeInternal(BigInteger obj, SerializationStream stream)
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

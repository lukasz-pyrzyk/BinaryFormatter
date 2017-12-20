using System;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class ByteArrayConverter : BaseTypeConverter<byte[]>
    {
        protected override void SerializeInternal(byte[] obj, SerializationStream stream)
        {
            stream.WriteWithLengthPrefix(obj);
        }

        protected override byte[] DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadBytesWithSizePrefix();
        }

        public override SerializedType Type => SerializedType.ByteArray;
    }
}

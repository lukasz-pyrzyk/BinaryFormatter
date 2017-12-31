using System;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class ByteConverter : BaseTypeConverter<byte>
    {
        protected override void SerializeInternal(byte obj, SerializationStream stream)
        {
            stream.Write(obj);
        }

        protected override byte DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadByte();
        }

        public override SerializedType Type => SerializedType.Byte;
    }
}

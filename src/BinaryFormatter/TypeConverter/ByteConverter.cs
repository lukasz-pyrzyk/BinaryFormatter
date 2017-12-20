using System;
using System.IO;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class ByteConverter : BaseTypeConverter<byte>
    {
        protected override void SerializeInternal(byte obj, Stream stream)
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

using System;
using System.IO;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class UShortConverter : BaseTypeConverter<ushort>
    {
        protected override void SerializeInternal(ushort obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override ushort DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadUShort();
        }

        public override SerializedType Type => SerializedType.UShort;
    }
}

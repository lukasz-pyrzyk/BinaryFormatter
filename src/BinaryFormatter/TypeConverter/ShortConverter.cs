using System;
using System.IO;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class ShortConverter : BaseTypeConverter<short>
    {
        protected override void SerializeInternal(short obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override short DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadShort();
        }

        public override SerializedType Type => SerializedType.Short;
    }
}

using System;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class ShortConverter : BaseTypeConverter<short>
    {
        protected override void SerializeInternal(short obj, SerializationStream stream)
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

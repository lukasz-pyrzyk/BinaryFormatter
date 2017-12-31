using System;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class SByteConverter : BaseTypeConverter<sbyte>
    {
        protected override void SerializeInternal(sbyte obj, SerializationStream stream)
        {
            stream.Write((byte)obj);
        }

        protected override sbyte DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadSByte();
        }

        public override SerializedType Type => SerializedType.Sbyte;
    }
}

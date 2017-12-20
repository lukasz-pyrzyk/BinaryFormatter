using System;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class FloatConverter : BaseTypeConverter<float>
    {
        protected override void SerializeInternal(float obj, SerializationStream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override float DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadFloat();
        }

        public override SerializedType Type => SerializedType.Float;
    }
}

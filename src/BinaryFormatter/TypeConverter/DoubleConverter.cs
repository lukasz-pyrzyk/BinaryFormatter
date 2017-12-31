using System;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class DoubleConverter : BaseTypeConverter<double>
    {
        protected override void SerializeInternal(double obj, SerializationStream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override double DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadDouble();
        }

        public override SerializedType Type => SerializedType.Double;
    }
}

using System;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class LongConverter : BaseTypeConverter<long>
    {
        protected override void SerializeInternal(long obj, SerializationStream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override long DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadLong();
        }

        public override SerializedType Type => SerializedType.Long;
    }
}

using System;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class IntConverter : BaseTypeConverter<int>
    {
        protected override void SerializeInternal(int obj, SerializationStream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override int DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadInt();
        }
        public override SerializedType Type => SerializedType.Int;
    }
}

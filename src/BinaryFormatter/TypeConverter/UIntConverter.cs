using System;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class UIntConverter : BaseTypeConverter<uint>
    {
        protected override void SerializeInternal(uint obj, SerializationStream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override uint DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadUInt();
        }

        public override SerializedType Type => SerializedType.Uint;
    }
}

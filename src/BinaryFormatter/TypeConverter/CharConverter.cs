using System;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class CharConverter : BaseTypeConverter<char>
    {
        protected override void SerializeInternal(char obj, SerializationStream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override char DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadChar();
        }

        public override SerializedType Type => SerializedType.Char;
    }
}

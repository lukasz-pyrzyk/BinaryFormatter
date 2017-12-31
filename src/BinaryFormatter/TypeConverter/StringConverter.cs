using System;
using System.Text;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class StringConverter : BaseTypeConverter<string>
    {
        protected override void SerializeInternal(string obj, SerializationStream stream)
        {
            byte[] objBytes = Encoding.UTF8.GetBytes(obj);
            stream.WriteWithLengthPrefix(objBytes);
        }

        protected override string DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadUtf8WithSizePrefix();
        }

        public override SerializedType Type => SerializedType.String;
    }
}
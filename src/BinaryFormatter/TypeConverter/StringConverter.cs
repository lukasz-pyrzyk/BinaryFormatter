using System;
using System.IO;
using System.Text;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class StringConverter : BaseTypeConverter<string>
    {
        protected override void SerializeInternal(string obj, Stream stream)
        {
            byte[] objBytes = Encoding.UTF8.GetBytes(obj);
            stream.WriteWithLengthPrefix(objBytes);
        }

        protected override string DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadUTF8WithSizePrefix();
        }

        public override SerializedType Type => SerializedType.String;
    }
}
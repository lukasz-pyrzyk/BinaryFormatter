using System;
using System.Text;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class UriConverter : BaseTypeConverter<Uri>
    {
        protected override void SerializeInternal(Uri obj, SerializationStream stream)
        {
            byte[] data = Encoding.UTF8.GetBytes(obj.AbsoluteUri);
            stream.WriteWithLengthPrefix(data);
        }

        protected override Uri DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            string absoluteUri = stream.ReadUtf8WithSizePrefix();
            return new Uri(absoluteUri);
        }

        public override SerializedType Type => SerializedType.Uri;
    }
}

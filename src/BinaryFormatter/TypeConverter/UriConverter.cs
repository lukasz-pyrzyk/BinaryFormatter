using System;
using System.IO;
using System.Text;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class UriConverter : BaseTypeConverter<Uri>
    {
        protected override void SerializeInternal(Uri obj, Stream stream)
        {
            byte[] data = Encoding.UTF8.GetBytes(obj.AbsoluteUri);
            stream.WriteWithLengthPrefix(data);
        }

        protected override Uri DeserializeInternal(WorkingStream stream, Type sourceType)
        {
            string absoluteUri = stream.ReadUTF8WithSizePrefix();
            return new Uri(absoluteUri);
        }

        public override SerializedType Type => SerializedType.Uri;
    }
}

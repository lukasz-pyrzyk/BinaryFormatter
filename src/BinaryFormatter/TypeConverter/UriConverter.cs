using System;
using System.IO;
using System.Text;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class UriConverter : BaseTypeConverter<Uri>
    {
        private int Size { get; set; }

        protected override void WriteObjectToStream(Uri obj, Stream stream)
        {
            byte[] data = Encoding.UTF8.GetBytes(obj.AbsoluteUri);
            stream.WriteWithLengthPrefix(data);

            Size = data.Length;
        }

        protected override Uri ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            string absoluteUri = stream.ReadUTF8WithSizePrefix();
            return new Uri(absoluteUri);
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.Uri;
    }
}

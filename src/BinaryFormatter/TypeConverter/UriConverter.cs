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

        protected override Uri ProcessDeserialize(byte[] stream, Type sourceType, ref int offset)
        {
            int dataSize = BitConverter.ToInt32(stream, offset);
            offset += sizeof(int);

            string AbsoluteUri = Encoding.UTF8.GetString(stream, offset, dataSize);

            return new Uri(AbsoluteUri);
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.Uri;
    }
}

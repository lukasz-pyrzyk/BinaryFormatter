using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class ByteArrayConverter : BaseTypeConverter<byte[]>
    {
        private int Size { get; set; }

        protected override void WriteObjectToStream(byte[] obj, Stream stream)
        {
            Size = obj.Length;

            stream.WriteWithLengthPrefix(obj);
        }

        protected override byte[] ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            return stream.ReadBytesWithSizePrefix();
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.ByteArray;
    }
}

using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class ByteArrayConverter : BaseTypeConverter<byte[]>
    {
        protected override void WriteObjectToStream(byte[] obj, Stream stream)
        {
            stream.WriteWithLengthPrefix(obj);
        }

        protected override byte[] ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            return stream.ReadBytesWithSizePrefix();
        }

        public override SerializedType Type => SerializedType.ByteArray;
    }
}

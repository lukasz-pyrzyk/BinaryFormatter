using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class ByteConverter : BaseTypeConverter<byte>
    {
        protected override void WriteObjectToStream(byte obj, Stream stream)
        {
            stream.Write(obj);
        }

        protected override byte ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            return stream.ReadByte();
        }

        protected override int GetTypeSize()
        {
            return sizeof(byte);
        }

        public override SerializedType Type => SerializedType.Byte;
    }
}

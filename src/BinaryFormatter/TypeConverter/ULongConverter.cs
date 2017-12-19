using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class ULongConverter : BaseTypeConverter<ulong>
    {
        protected override void WriteObjectToStream(ulong obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override ulong ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            return stream.ReadULong();
        }

        protected override int GetTypeSize()
        {
            return sizeof (ulong);
        }

        public override SerializedType Type => SerializedType.Ulong;
    }
}

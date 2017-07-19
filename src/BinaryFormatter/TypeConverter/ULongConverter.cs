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

        protected override ulong ProcessDeserialize(byte[] stream, Type sourceType, ref int offset)
        {
            return BitConverter.ToUInt64(stream, offset);
        }

        protected override int GetTypeSize()
        {
            return sizeof (ulong);
        }

        public override SerializedType Type => SerializedType.Ulong;
    }
}

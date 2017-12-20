using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class ULongConverter : BaseTypeConverter<ulong>
    {
        protected override void SerializeInternal(ulong obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override ulong DeserializeInternal(WorkingStream stream, Type sourceType)
        {
            return stream.ReadULong();
        }

        public override SerializedType Type => SerializedType.Ulong;
    }
}

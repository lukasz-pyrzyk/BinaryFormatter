using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class SByteConverter : BaseTypeConverter<sbyte>
    {
        protected override void WriteObjectToStream(sbyte obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override sbyte ProcessDeserialize(byte[] bytes, Type sourceType, ref int offset)
        {
            return (sbyte)BitConverter.ToInt16(bytes, offset);
        }

        protected override int GetTypeSize()
        {
            return sizeof (sbyte);
        }

        public override SerializedType Type => SerializedType.Sbyte;
    }
}

using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class ShortConverter : BaseTypeConverter<short>
    {
        protected override void WriteObjectToStream(short obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override short ProcessDeserialize(byte[] bytes, Type sourceType, ref int offset)
        {
            return BitConverter.ToInt16(bytes, offset);
        }

        protected override int GetTypeSize()
        {
            return sizeof (short);
        }

        public override SerializedType Type => SerializedType.Short;
    }
}

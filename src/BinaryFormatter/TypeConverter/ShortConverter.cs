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

        protected override short ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            return stream.ReadShort();
        }

        protected override int GetTypeSize()
        {
            return sizeof (short);
        }

        public override SerializedType Type => SerializedType.Short;
    }
}

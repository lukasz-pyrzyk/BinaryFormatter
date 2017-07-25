using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class BoolConverter : BaseTypeConverter<bool>
    {
        protected override void WriteObjectToStream(bool obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override bool ProcessDeserialize(byte[] bytes, Type sourceType, ref int offset)
        {
            bool result = BitConverter.ToBoolean(bytes, offset);
            return result;
        }

        protected override int GetTypeSize()
        {
            return sizeof (bool);
        }

        public override SerializedType Type => SerializedType.Bool;
    }
}

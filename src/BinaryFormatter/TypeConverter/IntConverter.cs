using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class IntConverter : BaseTypeConverter<int>
    {
        protected override void SerializeInternal(int obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override int DeserializeInternal(WorkingStream stream, Type sourceType)
        {
            return stream.ReadInt();
        }
        public override SerializedType Type => SerializedType.Int;
    }
}

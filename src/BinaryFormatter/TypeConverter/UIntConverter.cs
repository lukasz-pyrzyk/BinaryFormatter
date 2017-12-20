using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class UIntConverter : BaseTypeConverter<uint>
    {
        protected override void SerializeInternal(uint obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override uint DeserializeInternal(WorkingStream stream, Type sourceType)
        {
            return stream.ReadUInt();
        }

        public override SerializedType Type => SerializedType.Uint;
    }
}

using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class UIntConverter : BaseTypeConverter<uint>
    {
        protected override void WriteObjectToStream(uint obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override uint ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            return stream.ReadUInt();
        }

        protected override int GetTypeSize()
        {
            return sizeof (uint);
        }

        public override SerializedType Type => SerializedType.Uint;
    }
}

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

        protected override uint ProcessDeserialize(byte[] stream, Type sourceType, ref int offset)
        {
            return BitConverter.ToUInt32(stream, offset);
        }

        protected override int GetTypeSize()
        {
            return sizeof (uint);
        }

        public override SerializedType Type => SerializedType.Uint;
    }
}

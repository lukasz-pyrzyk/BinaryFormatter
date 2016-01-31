using System;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class ByteConverter : BaseTypeConverter<byte>
    {
        protected override byte[] ProcessSerialize(byte obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override byte ProcessDeserialize(byte[] stream, ref int offset)
        {
            return (byte)BitConverter.ToUInt16(stream, offset);
        }

        protected override int GetTypeSize()
        {
            return sizeof (byte);
        }

        public override SerializedType Type => SerializedType.Byte;
    }
}

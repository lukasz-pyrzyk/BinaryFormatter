using System;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class UShortConverter : BaseTypeConverter<ushort>
    {
        protected override byte[] ProcessSerialize(ushort obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override ushort ProcessDeserialize(byte[] stream, ref int offset)
        {
            return BitConverter.ToUInt16(stream, offset);
        }

        protected override int GetTypeSize()
        {
            return sizeof (ushort);
        }

        public override SerializedType Type => SerializedType.UShort;
    }
}

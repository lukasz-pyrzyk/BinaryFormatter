using System;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class ULongConverter : BaseTypeConverter<ulong>
    {
        protected override byte[] ProcessSerialize(ulong obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override ulong ProcessDeserialize(byte[] stream, ref int offset)
        {
            return BitConverter.ToUInt64(stream, offset);
        }

        protected override int GetTypeSize()
        {
            return sizeof (ulong);
        }

        public override SerializedType Type => SerializedType.Ulong;
    }
}

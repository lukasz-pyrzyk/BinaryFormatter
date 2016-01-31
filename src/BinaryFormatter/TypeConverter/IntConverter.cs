using System;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class IntConverter : BaseTypeConverter<int>
    {
        protected override byte[] ProcessSerialize(int obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override int ProcessDeserialize(byte[] stream, ref int offset)
        {
            return BitConverter.ToInt32(stream, offset);
        }

        protected override int GetTypeSize()
        {
            return sizeof (int);
        }

        public override SerializedType Type => SerializedType.Int;
    }
}

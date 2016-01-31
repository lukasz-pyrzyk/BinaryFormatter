using System;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class LongConverter : BaseTypeConverter<long>
    {
        protected override byte[] ProcessSerialize(long obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override long ProcessDeserialize(byte[] stream, ref int offset)
        {
            return BitConverter.ToInt64(stream, offset);
        }

        protected override int GetTypeSize()
        {
            return sizeof (long);
        }

        public override SerializedType Type => SerializedType.Long;
    }
}

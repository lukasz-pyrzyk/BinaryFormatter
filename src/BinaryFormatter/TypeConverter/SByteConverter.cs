using System;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class SByteConverter : BaseTypeConverter<sbyte>
    {
        protected override byte[] ProcessSerialize(sbyte obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override sbyte ProcessDeserialize(byte[] stream, ref int offset)
        {
            return (sbyte)BitConverter.ToInt16(stream, offset);
        }

        protected override int GetTypeSize()
        {
            return sizeof (sbyte);
        }

        public override SerializedType Type => SerializedType.Sbyte;
    }
}

using System;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class DoubleConverter : BaseTypeConverter<double>
    {
        protected override byte[] ProcessSerialize(double obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override double ProcessDeserialize(byte[] stream, ref int offset)
        {
            return BitConverter.ToDouble(stream, offset);
        }

        protected override int GetTypeSize()
        {
            return sizeof (double);
        }

        public override SerializedType Type => SerializedType.Double;
    }
}

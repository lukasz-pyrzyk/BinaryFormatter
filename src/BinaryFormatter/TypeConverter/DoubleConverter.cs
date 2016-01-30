using System;

namespace BinaryFormatter.TypeConverter
{
    internal class DoubleConverter : BaseTypeConverter<double>
    {
        protected override byte[] ProcessSerialize(double obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override int GetTypeSize()
        {
            return sizeof (double);
        }
    }
}

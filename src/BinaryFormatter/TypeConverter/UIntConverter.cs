using System;

namespace BinaryFormatter.TypeConverter
{
    internal class UIntConverter : BaseTypeConverter<uint>
    {
        protected override byte[] ProcessSerialize(uint obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override int GetTypeSize()
        {
            return sizeof (uint);
        }
    }
}

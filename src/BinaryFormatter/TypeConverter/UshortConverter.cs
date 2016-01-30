using System;

namespace BinaryFormatter.TypeConverter
{
    internal class UShortConverter : BaseTypeConverter<ushort>
    {
        protected override byte[] ProcessSerialize(ushort obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override int GetTypeSize()
        {
            return sizeof (ushort);
        }
    }
}

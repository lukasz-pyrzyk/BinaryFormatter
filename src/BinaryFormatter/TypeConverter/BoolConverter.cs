using System;

namespace BinaryFormatter.TypeConverter
{
    internal class BoolConverter : BaseTypeConverter<bool>
    {
        protected override byte[] ProcessSerialize(bool obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override int GetTypeSize()
        {
            return sizeof (bool);
        }
    }
}

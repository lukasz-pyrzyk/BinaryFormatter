using System;

namespace BinaryFormatter.TypeConverter
{
    internal class ShortConverter : BaseTypeConverter<short>
    {
        protected override byte[] ProcessSerialize(short obj)
        {
            return BitConverter.GetBytes(obj);
        }
    }
}

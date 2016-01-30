using System;

namespace BinaryFormatter.TypeConverter
{
    internal class UshortConverter : BaseTypeConverter<ushort>
    {
        protected override byte[] ProcessSerialize(ushort obj)
        {
            return BitConverter.GetBytes(obj);
        }
    }
}

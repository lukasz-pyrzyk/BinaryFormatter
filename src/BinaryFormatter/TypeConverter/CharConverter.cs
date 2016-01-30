using System;

namespace BinaryFormatter.TypeConverter
{
    internal class CharConverter : BaseTypeConverter<char>
    {
        protected override byte[] ProcessSerialize(char obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override int GetTypeSize()
        {
            return sizeof(char);
        }
    }
}

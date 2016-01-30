using System;

namespace BinaryFormatter.TypeConverter
{
    internal class IntConverter : BaseTypeConverter<int>
    {
        protected override byte[] ProcessSerialize(int obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override int GetTypeSize()
        {
            return sizeof (int);
        }
    }
}

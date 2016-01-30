using System;
using System.Text;

namespace BinaryFormatter.TypeConverter
{
    internal class DecimalConverter : BaseTypeConverter<decimal>
    {
        private int Size { get; set; } = 0;

        protected override byte[] ProcessSerialize(decimal obj)
        {
            string sdecimal = obj.ToString("F");
            Size = sdecimal.Length;
            return Encoding.UTF8.GetBytes(sdecimal);
        }

        protected override int GetTypeSize()
        {
            return Size;
        }
    }
}

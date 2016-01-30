using System.Text;

namespace BinaryFormatter.TypeConverter
{
    internal class StringConverter : BaseTypeConverter<string>
    {
        private int Size { get; set; } = 0;

        protected override byte[] ProcessSerialize(string obj)
        {
            Size = obj.Length;
            return Encoding.UTF8.GetBytes(obj);
        }

        protected override int GetTypeSize()
        {
            return Size;
        }
    }
}

using System;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class CharConverter : BaseTypeConverter<char>
    {
        protected override byte[] ProcessSerialize(char obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override char ProcessDeserialize(byte[] stream, ref int offset)
        {
            char result = BitConverter.ToChar(stream, offset);
            return result;
        }

        protected override int GetTypeSize()
        {
            return sizeof(char);
        }

        public override SerializedType Type => SerializedType.Char;
    }
}

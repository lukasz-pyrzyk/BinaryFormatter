using System;

namespace BinaryFormatter.TypeConverter
{
    internal class ByteConverter : BaseTypeConverter<byte>
    {
        protected override byte[] ProcessSerialize(byte obj)
        {
            return BitConverter.GetBytes(obj);
        }

        protected override int GetTypeSize()
        {
            return sizeof (byte);
        }
    }
}

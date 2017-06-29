using System;
using System.Text;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class StringConverter : BaseTypeConverter<string>
    {
        private int Size { get; set; }

        protected override byte[] ProcessSerialize(string obj)
        {
            byte[] objBytes = Encoding.UTF8.GetBytes(obj);
            byte[] sizeBytes = BitConverter.GetBytes(objBytes.Length);
            Size = objBytes.Length;

            byte[] serializedStringWithSize = new byte[sizeof(int) + objBytes.Length];

            Array.ConstrainedCopy(sizeBytes, 0, serializedStringWithSize, 0, sizeBytes.Length);
            Array.ConstrainedCopy(objBytes, 0, serializedStringWithSize, sizeBytes.Length, objBytes.Length);

            return serializedStringWithSize;
        }

        protected override string ProcessDeserialize(byte[] stream, ref int offset)
        {
            Size = BitConverter.ToInt32(stream, offset);
            offset += sizeof(int);

            return Encoding.UTF8.GetString(stream, offset, Size);
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.String;
    }
}
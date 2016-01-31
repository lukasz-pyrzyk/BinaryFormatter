using System;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class ByteArrayConverter : BaseTypeConverter<byte[]>
    {
        private int Size { get; set; }

        protected override byte[] ProcessSerialize(byte[] obj)
        {
            Size = obj.Length;
            byte[] lengthBytes = BitConverter.GetBytes(Size);

            byte[] serializedStringWithSize = new byte[sizeof(int) + Size];
            Array.Copy(lengthBytes, 0, serializedStringWithSize, 0, lengthBytes.Length);
            Array.Copy(obj, 0, serializedStringWithSize, lengthBytes.Length, obj.Length);

            return serializedStringWithSize;
        }

        protected override byte[] ProcessDeserialize(byte[] stream, ref int offset)
        {
            int size = BitConverter.ToInt32(stream, offset);
            offset += sizeof(int);

            byte[] deserialized = new byte[size];
            Array.Copy(stream, offset, deserialized, 0, size);
            return deserialized;
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.ByteArray;
    }
}

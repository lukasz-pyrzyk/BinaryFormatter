using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class GuidConverter : BaseTypeConverter<Guid>
    {
        private int Size { get; set; }

        protected override void WriteObjectToStream(Guid obj, Stream stream)
        {
            byte[] data = obj.ToByteArray();
            stream.WriteWithLengthPrefix(data);

            Size = data.Length;
        }

        protected override Guid ProcessDeserialize(byte[] bytes, Type sourceType, ref int offset)
        {
            int dataSize = BitConverter.ToInt32(bytes, offset);
            offset += sizeof(int);

            byte[] guidData = new byte[dataSize];
            Array.Copy(bytes, offset, guidData, 0, dataSize);

            return new Guid(guidData);
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.Guid;
    }
}

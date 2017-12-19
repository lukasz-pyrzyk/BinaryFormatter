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

        protected override Guid ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            byte[] guidData = stream.ReadBytesWithSizePrefix();

            return new Guid(guidData);
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.Guid;
    }
}

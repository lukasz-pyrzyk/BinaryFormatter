using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class GuidConverter : BaseTypeConverter<Guid>
    {
        protected override void SerializeInternal(Guid obj, Stream stream)
        {
            byte[] data = obj.ToByteArray();
            stream.WriteWithLengthPrefix(data);
        }

        protected override Guid DeserializeInternal(WorkingStream stream, Type sourceType)
        {
            byte[] guidData = stream.ReadBytesWithSizePrefix();

            return new Guid(guidData);
        }

        public override SerializedType Type => SerializedType.Guid;
    }
}

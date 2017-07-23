using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class GuidConverter : BaseTypeConverter<Guid>
    {
        protected override void WriteObjectToStream(Guid obj, Stream stream)
        {
            byte[] data = obj.ToByteArray();
            stream.Write(data);
        }

        protected override Guid ProcessDeserialize(byte[] stream, Type sourceType, ref int offset)
        {
            byte[] guidData = new byte[16];
            Array.Copy(stream, offset, guidData, 0, 16);

            return new Guid(guidData);
        }

        protected override int GetTypeSize()
        {
            return 16;
        }

        public override SerializedType Type => SerializedType.Guid;
    }
}

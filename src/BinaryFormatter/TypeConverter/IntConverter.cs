using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class IntConverter : BaseTypeConverter<int>
    {
        protected override void WriteObjectToStream(int obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override int ProcessDeserialize(byte[] stream, ref int offset)
        {
            return BitConverter.ToInt32(stream, offset);
        }

        protected override int GetTypeSize()
        {
            return sizeof (int);
        }

        public override SerializedType Type => SerializedType.Int;
    }
}

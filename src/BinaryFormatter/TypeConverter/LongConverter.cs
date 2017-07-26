using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class LongConverter : BaseTypeConverter<long>
    {
        protected override void WriteObjectToStream(long obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override long ProcessDeserialize(byte[] bytes, Type sourceType, ref int offset)
        {
            return BitConverter.ToInt64(bytes, offset);
        }

        protected override int GetTypeSize()
        {
            return sizeof (long);
        }

        public override SerializedType Type => SerializedType.Long;
    }
}

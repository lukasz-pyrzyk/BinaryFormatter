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

        protected override long ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            return stream.ReadLong();
        }

        protected override int GetTypeSize()
        {
            return sizeof (long);
        }

        public override SerializedType Type => SerializedType.Long;
    }
}

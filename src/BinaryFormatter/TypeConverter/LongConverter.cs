using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class LongConverter : BaseTypeConverter<long>
    {
        protected override void SerializeInternal(long obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override long DeserializeInternal(WorkingStream stream, Type sourceType)
        {
            return stream.ReadLong();
        }

        public override SerializedType Type => SerializedType.Long;
    }
}

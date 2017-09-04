using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class TimespanConverter : BaseTypeConverter<TimeSpan>
    {
        protected override void WriteObjectToStream(TimeSpan obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj.Ticks);
            stream.Write(data);
        }

        protected override TimeSpan ProcessDeserialize(byte[] bytes, Type sourceType, ref int offset)
        {
            long ticks = BitConverter.ToInt64(bytes, offset);
            return TimeSpan.FromTicks(ticks);
        }

        protected override int GetTypeSize()
        {
            return sizeof (long);
        }

        public override SerializedType Type => SerializedType.Timespan;
    }
}

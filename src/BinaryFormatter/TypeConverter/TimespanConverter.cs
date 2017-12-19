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

        protected override TimeSpan ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            long ticks = stream.ReadLong();
            return TimeSpan.FromTicks(ticks);
        }

        public override SerializedType Type => SerializedType.Timespan;
    }
}

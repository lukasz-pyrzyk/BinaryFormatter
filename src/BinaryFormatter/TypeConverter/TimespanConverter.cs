using System;
using System.IO;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class TimespanConverter : BaseTypeConverter<TimeSpan>
    {
        protected override void SerializeInternal(TimeSpan obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj.Ticks);
            stream.Write(data);
        }

        protected override TimeSpan DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            long ticks = stream.ReadLong();
            return TimeSpan.FromTicks(ticks);
        }

        public override SerializedType Type => SerializedType.Timespan;
    }
}

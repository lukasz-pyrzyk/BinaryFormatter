using System;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class DatetimeConverter : BaseTypeConverter<DateTime>
    {
        protected override void SerializeInternal(DateTime obj, SerializationStream stream)
        {
            byte[] data = BitConverter.GetBytes(obj.Ticks);
            stream.Write(data);
        }

        protected override DateTime DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            long ticks = stream.ReadLong();
            return DateTime.FromBinary(ticks);
        }

        public override SerializedType Type => SerializedType.Datetime;
    }
}

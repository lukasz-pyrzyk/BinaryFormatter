using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class DatetimeConverter : BaseTypeConverter<DateTime>
    {
        protected override void WriteObjectToStream(DateTime obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj.Ticks);
            stream.Write(data);
        }

        protected override DateTime ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            long ticks = stream.ReadLong();
            return DateTime.FromBinary(ticks);
        }

        protected override int GetTypeSize()
        {
            return sizeof (long);
        }

        public override SerializedType Type => SerializedType.Datetime;
    }
}

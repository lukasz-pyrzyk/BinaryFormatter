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

        protected override DateTime ProcessDeserialize(byte[] stream, Type sourceType, ref int offset)
        {
            long ticks = BitConverter.ToInt64(stream, offset);
            return DateTime.FromBinary(ticks);
        }

        protected override int GetTypeSize()
        {
            return sizeof (long);
        }

        public override SerializedType Type => SerializedType.Datetime;
    }
}

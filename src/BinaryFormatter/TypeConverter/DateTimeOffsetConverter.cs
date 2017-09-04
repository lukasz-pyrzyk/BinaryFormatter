using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class DateTimeOffsetConverter : BaseTypeConverter<DateTimeOffset>
    {
        protected override void WriteObjectToStream(DateTimeOffset obj, Stream stream)
        {
            BinaryConverter converter = new BinaryConverter();
            byte[] dataDateTime = converter.Serialize(obj.DateTime);
            stream.WriteWithLengthPrefix(dataDateTime);
            byte[] dataOffsetTimeSpan = converter.Serialize(obj.Offset);
            stream.WriteWithLengthPrefix(dataOffsetTimeSpan);
        }

        protected override DateTimeOffset ProcessDeserialize(byte[] bytes, Type sourceType, ref int offset)
        {
            BinaryConverter converter = new BinaryConverter();

            int dataDateTimeSize = BitConverter.ToInt32(bytes, offset);
            offset += sizeof(int);
            byte[] dataDateTime = new byte[dataDateTimeSize];
            Array.Copy(bytes, offset, dataDateTime, 0, dataDateTimeSize);
            var deserializedDateTime = converter.Deserialize<DateTime>(dataDateTime);
            offset += dataDateTime.Length;

            int dataOffsetTimeSpanSize = BitConverter.ToInt32(bytes, offset);
            offset += sizeof(int);
            byte[] dataOffsetTimeSpan = new byte[dataOffsetTimeSpanSize];
            Array.Copy(bytes, offset, dataOffsetTimeSpan, 0, dataOffsetTimeSpanSize);
            var deserializedTimeSpan = converter.Deserialize<TimeSpan>(dataOffsetTimeSpan);
            offset += dataOffsetTimeSpan.Length;
            return new DateTimeOffset(deserializedDateTime, deserializedTimeSpan);
        }

        protected override int GetTypeSize()
        {
            return sizeof (long);
        }

        public override SerializedType Type => SerializedType.DateTimeOffset;
    }
}

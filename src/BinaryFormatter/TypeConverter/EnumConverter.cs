using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class EnumConverter : BaseTypeConverter<Enum>
    {
        private int Size { get; set; }

        protected override void WriteObjectToStream(Enum obj, Stream stream)
        {
            Type enumUnderlyingType = Enum.GetUnderlyingType(obj.GetType());
            var underlyingValue = Convert.ChangeType(obj, enumUnderlyingType);

            BinaryConverter converter = new BinaryConverter();
            byte[] data = converter.Serialize(underlyingValue);
            stream.WriteWithLengthPrefix(data);

            Size = data.Length;
        }

        protected override Enum ProcessDeserialize(byte[] stream, Type sourceType, ref int offset)
        {
            var ws = new WorkingStream(stream, offset);
            Enum result = DeserializeInto(ws, sourceType);
            offset = ws.Offset;

            return result;
        }

        public Enum DeserializeInto(WorkingStream stream, Type type)
        {
            byte[] enumData = stream.ReadBytesWithSizePrefix();

            BinaryConverter converter = new BinaryConverter();
            var underlyingValue = converter.Deserialize<object>(enumData);

            return (Enum)Enum.ToObject(type, underlyingValue);
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.Enum;
    }
}

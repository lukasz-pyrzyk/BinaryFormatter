using System;
using System.IO;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class EnumConverter : BaseTypeConverter<Enum>
    {
        protected override void SerializeInternal(Enum obj, Stream stream)
        {
            Type enumUnderlyingType = Enum.GetUnderlyingType(obj.GetType());
            var underlyingValue = Convert.ChangeType(obj, enumUnderlyingType);

            BinaryConverter converter = new BinaryConverter();
            byte[] data = converter.Serialize(underlyingValue);
            stream.WriteWithLengthPrefix(data);
        }

        protected override Enum DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            Enum result = DeserializeInto(stream, sourceType);
            return result;
        }

        public Enum DeserializeInto(DeserializationStream stream, Type type)
        {
            byte[] enumData = stream.ReadBytesWithSizePrefix();

            BinaryConverter converter = new BinaryConverter();
            var underlyingValue = converter.Deserialize<object>(enumData);

            return (Enum)Enum.ToObject(type, underlyingValue);
        }

        public override SerializedType Type => SerializedType.Enum;
    }
}

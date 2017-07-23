using System;
using System.IO;
using System.Text;
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
            int dataSize = BitConverter.ToInt32(stream, offset);
            offset += sizeof(int);

            byte[] enumData = new byte[dataSize];
            Array.Copy(stream, offset, enumData, 0, dataSize);

            BinaryConverter converter = new BinaryConverter();
            var underlyingValue = converter.Deserialize<object>(enumData);

            return (Enum)Enum.ToObject(sourceType, underlyingValue);
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.Enum;
    }
}

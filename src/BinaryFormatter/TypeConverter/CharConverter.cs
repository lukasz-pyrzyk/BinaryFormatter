using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class CharConverter : BaseTypeConverter<char>
    {
        protected override void WriteObjectToStream(char obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override char ProcessDeserialize(byte[] stream, Type sourceType, ref int offset)
        {
            char result = BitConverter.ToChar(stream, offset);
            return result;
        }

        protected override int GetTypeSize()
        {
            return sizeof(char);
        }

        public override SerializedType Type => SerializedType.Char;
    }
}

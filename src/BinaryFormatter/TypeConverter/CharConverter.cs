using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class CharConverter : BaseTypeConverter<char>
    {
        protected override void SerializeInternal(char obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override char DeserializeInternal(WorkingStream stream, Type sourceType)
        {
            return stream.ReadChar();
        }

        public override SerializedType Type => SerializedType.Char;
    }
}

using System;
using System.IO;
using System.Text;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class StringConverter : BaseTypeConverter<string>
    {
        private int Size { get; set; }

        protected override void WriteObjectToStream(string obj, Stream stream)
        {
            byte[] objBytes = Encoding.UTF8.GetBytes(obj);
            byte[] sizeBytes = BitConverter.GetBytes(objBytes.Length);
            Size = objBytes.Length;

            stream.Write(sizeBytes);
            stream.Write(objBytes);
        }

        protected override string ProcessDeserialize(byte[] stream, ref int offset)
        {
            Size = BitConverter.ToInt32(stream, offset);
            offset += sizeof (int);

            return Encoding.UTF8.GetString(stream, offset, Size);
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.String;
    }
}
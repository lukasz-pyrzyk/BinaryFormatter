using System;
using System.IO;
using System.Text;
using BinaryFormatter.TypeConverter;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class BaseTypeConverterTests
    {
        public const string Message = "Lorem ipsum";
        
        internal class Fake : BaseTypeConverter<string>
        {
            protected override int GetTypeSize()
            {
                return Message.Length;
            }

            protected override void WriteObjectToStream(string obj, Stream stream)
            {
                var data = Encoding.UTF8.GetBytes(obj);
                stream.Write(data);
            }

            protected override string ProcessDeserialize(byte[] stream, Type sourceType, ref int offset)
            {
                int size = BitConverter.ToInt32(stream, offset);
                offset += sizeof (int);
                return Encoding.UTF8.GetString(stream, offset, size);
            }

            public override SerializedType Type => SerializedType.String;
        }
    }
}
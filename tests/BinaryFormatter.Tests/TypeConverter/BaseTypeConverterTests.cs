using System;
using System.IO;
using System.Text;
using BinaryFormatter;
using BinaryFormatter.TypeConverter;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class BaseTypeConverterTests
    {
        public const string Message = "Lorem ipsum";
        
        internal class Fake : BaseTypeConverter<string>
        {
            protected override void WriteObjectToStream(string obj, Stream stream)
            {
                var data = Encoding.UTF8.GetBytes(obj);
                stream.WriteWithLengthPrefix(data);
            }

            protected override string ProcessDeserialize(WorkingStream stream, Type sourceType)
            {
                return stream.ReadUTF8WithSizePrefix();
            }

            public override SerializedType Type => SerializedType.String;
        }
    }
}
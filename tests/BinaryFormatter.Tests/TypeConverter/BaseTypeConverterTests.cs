using System;
using System.Text;
using BinaryFormatter.Streams;
using BinaryFormatter.TypeConverter;
using BinaryFormatter.Types;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class BaseTypeConverterTests
    {
        public const string Message = "Lorem ipsum";
        
        internal class Fake : BaseTypeConverter<string>
        {
            protected override void SerializeInternal(string obj, SerializationStream stream)
            {
                var data = Encoding.UTF8.GetBytes(obj);
                stream.WriteWithLengthPrefix(data);
            }

            protected override string DeserializeInternal(DeserializationStream stream, Type sourceType)
            {
                return stream.ReadUTF8WithSizePrefix();
            }

            public override SerializedType Type => SerializedType.String;
        }
    }
}
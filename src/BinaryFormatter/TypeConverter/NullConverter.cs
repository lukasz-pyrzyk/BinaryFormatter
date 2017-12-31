using System;
using BinaryFormatter.Types;
using BinaryFormatter.Streams;

namespace BinaryFormatter.TypeConverter
{
    internal class NullConverter : BaseTypeConverter<object>
    {

        protected override void SerializeInternal(object obj, SerializationStream stream)
        {
            stream.Write();
        }

        protected override object DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return null;
        }

        public override SerializedType Type => SerializedType.Null;
    }
}

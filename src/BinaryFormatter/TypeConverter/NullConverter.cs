using System;
using BinaryFormatter.Types;
using System.IO;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class NullConverter : BaseTypeConverter<object>
    {

        protected override void SerializeInternal(object obj, Stream stream)
        {
            stream.Write(new byte[0]);
        }

        protected override object DeserializeInternal(WorkingStream stream, Type sourceType)
        {
            return null;
        }

        public override SerializedType Type => SerializedType.Null;
    }
}

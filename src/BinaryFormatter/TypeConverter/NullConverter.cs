using System;
using BinaryFormatter.Types;
using System.IO;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class NullConverter : BaseTypeConverter<object>
    {
        private int Size { get; set; }

        protected override void WriteObjectToStream(object obj, Stream stream)
        {
            stream.Write(new byte[0]);
        }

        protected override object ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            return null;
        }

        protected override int GetTypeSize()
        {
            return 0;
        }

        public override SerializedType Type => SerializedType.Null;
    }
}

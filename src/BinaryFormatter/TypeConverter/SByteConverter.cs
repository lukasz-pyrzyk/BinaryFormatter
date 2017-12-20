using System;
using System.IO;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class SByteConverter : BaseTypeConverter<sbyte>
    {
        protected override void SerializeInternal(sbyte obj, Stream stream)
        {
            stream.Write((byte)obj);
        }

        protected override sbyte DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadSByte();
        }

        public override SerializedType Type => SerializedType.Sbyte;
    }
}

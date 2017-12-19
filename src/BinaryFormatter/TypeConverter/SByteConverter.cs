using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class SByteConverter : BaseTypeConverter<sbyte>
    {
        protected override void WriteObjectToStream(sbyte obj, Stream stream)
        {
            stream.Write((byte)obj);
        }

        protected override sbyte ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            return stream.ReadSByte();
        }

        protected override int GetTypeSize()
        {
            return sizeof (sbyte);
        }

        public override SerializedType Type => SerializedType.Sbyte;
    }
}

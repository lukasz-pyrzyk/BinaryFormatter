using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class DoubleConverter : BaseTypeConverter<double>
    {
        protected override void SerializeInternal(double obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override double DeserializeInternal(WorkingStream stream, Type sourceType)
        {
            return stream.ReadDouble();
        }

        public override SerializedType Type => SerializedType.Double;
    }
}

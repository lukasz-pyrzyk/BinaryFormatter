using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class DoubleConverter : BaseTypeConverter<double>
    {
        protected override void WriteObjectToStream(double obj, Stream stream)
        {
            byte[] data = BitConverter.GetBytes(obj);
            stream.Write(data);
        }

        protected override double ProcessDeserialize(byte[] stream, Type sourceType, ref int offset)
        {
            return BitConverter.ToDouble(stream, offset);
        }

        protected override int GetTypeSize()
        {
            return sizeof (double);
        }

        public override SerializedType Type => SerializedType.Double;
    }
}

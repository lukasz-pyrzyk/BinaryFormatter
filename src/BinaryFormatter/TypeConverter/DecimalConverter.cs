using System;
using System.IO;
using BinaryFormatter.Streams;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class DecimalConverter : BaseTypeConverter<decimal>
    {
        protected override void SerializeInternal(decimal obj, Stream stream)
        {
            int[] bits = decimal.GetBits(obj);
            foreach (int bit in bits)
            {
                byte[] data = BitConverter.GetBytes(bit);
                stream.Write(data);
            }
        }

        protected override decimal DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            return stream.ReadDecimal();
        }

        public override SerializedType Type => SerializedType.Decimal;
    }
}

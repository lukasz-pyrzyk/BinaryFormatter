using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class DecimalConverter : BaseTypeConverter<decimal>
    {
        private int Size { get; set; } = 0;

        protected override void WriteObjectToStream(decimal obj, Stream stream)
        {
            int[] bits = decimal.GetBits(obj);
            foreach (int bit in bits)
            {
                byte[] data = BitConverter.GetBytes(bit);
                stream.Write(data);
            }
        }

        protected override decimal ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            return stream.ReadDecimal();
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.Decimal;
    }
}

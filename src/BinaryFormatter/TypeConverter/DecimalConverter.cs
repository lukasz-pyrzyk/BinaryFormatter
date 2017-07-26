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

        protected override decimal ProcessDeserialize(byte[] bytes, Type sourceType, ref int offset)
        {
            var bits = new int[4];
            for (int i = 0; i < 4; i++)
            {
                bits[i] = BitConverter.ToInt32(bytes, offset);
                offset += sizeof(int);
            }

            return new decimal(bits);
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.Decimal;
    }
}

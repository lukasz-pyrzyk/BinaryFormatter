using System;
using System.IO;
using System.Linq;
using System.Text;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class DecimalConverter : BaseTypeConverter<decimal>
    {
        private int Size { get; set; } = 0;

        protected override byte[] ProcessSerialize(decimal obj)
        {
            string sdecimal = obj.ToString("F");
            Size = sdecimal.Length;
            var ms = new MemoryStream();

            new StringConverter().Serialize(sdecimal, ms);
            return ms.ToArray();
        }

        protected override decimal ProcessDeserialize(byte[] stream, ref int offset)
        {
            string sdecimal = new StringConverter().Deserialize(stream.Skip(offset).ToArray());
            return decimal.Parse(sdecimal);
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.Decimal;
    }
}

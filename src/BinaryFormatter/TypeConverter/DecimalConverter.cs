using System.IO;
using System.Linq;
using BinaryFormatter.Types;

namespace BinaryFormatter.TypeConverter
{
    internal class DecimalConverter : BaseTypeConverter<decimal>
    {
        private int Size { get; set; } = 0;

        protected override void WriteObjectToStream(decimal obj, Stream stream)
        {
            string sdecimal = obj.ToString("F");
            Size = sdecimal.Length;

            new StringConverter().Serialize(sdecimal, stream);
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

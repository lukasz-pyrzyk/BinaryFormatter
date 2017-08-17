using System;
using System.IO;
using System.Text;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;
using System.Collections.Generic;
using System.Reflection;

namespace BinaryFormatter.TypeConverter
{
    internal class KeyValuePairConverter : BaseTypeConverter<object>
    {
        private int Size { get; set; }

        protected override void WriteObjectToStream(object obj, Stream stream)
        {
            BinaryConverter converter = new BinaryConverter();

            Type baseType = obj.GetType().GetGenericTypeDefinition();
            TypeInfo baseTypeInfo = baseType.GetTypeInfo();
            KeyValuePair<object, object> objAsKeyValuePair = TypeHelper.CastFrom(obj);

            byte[] dataKey = converter.Serialize(objAsKeyValuePair.Key);            
            stream.WriteWithLengthPrefix(dataKey);          

            byte[] dataValue = converter.Serialize(objAsKeyValuePair.Value);
            stream.WriteWithLengthPrefix(dataValue);

            Size = dataKey.Length + dataValue.Length;
        }

        protected override object ProcessDeserialize(byte[] stream, Type sourceType, ref int offset)
        {
            BinaryConverter converter = new BinaryConverter();      

            int dataKeySize = BitConverter.ToInt32(stream, offset);
            offset += sizeof(int);
            byte[] keyData = new byte[dataKeySize];
            Array.Copy(stream, offset, keyData, 0, dataKeySize);
            var deserializedKey = converter.Deserialize<object>(keyData);
            offset += keyData.Length;

            int dataValueSize = BitConverter.ToInt32(stream, offset);
            offset += sizeof(int);
            byte[] valueData = new byte[dataValueSize];
            Array.Copy(stream, offset, valueData, 0, dataValueSize);
            var deserializedValue = converter.Deserialize<object>(valueData);
            offset += keyData.Length;

            var newKeyValuePair = Activator.CreateInstance(sourceType, new[] { deserializedKey, deserializedValue });            
            return newKeyValuePair;
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.KeyValuePair;
    }
}

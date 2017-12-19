using System;
using System.IO;
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

        protected override object ProcessDeserialize(WorkingStream stream, Type sourceType)
        {
            BinaryConverter converter = new BinaryConverter();

            byte[] keyData = stream.ReadBytesWithSizePrefix();
            var deserializedKey = converter.Deserialize<object>(keyData);

            byte[] valueData = stream.ReadBytesWithSizePrefix();
            var deserializedValue = converter.Deserialize<object>(valueData);

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

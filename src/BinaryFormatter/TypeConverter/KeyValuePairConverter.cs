using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;
using System.Collections.Generic;

namespace BinaryFormatter.TypeConverter
{
    internal class KeyValuePairConverter : BaseTypeConverter<object>
    {
        protected override void WriteObjectToStream(object obj, Stream stream)
        {
            BinaryConverter converter = new BinaryConverter();
            KeyValuePair<object, object> objAsKeyValuePair = TypeHelper.CastFrom(obj);

            byte[] dataKey = converter.Serialize(objAsKeyValuePair.Key);            
            stream.WriteWithLengthPrefix(dataKey);          

            byte[] dataValue = converter.Serialize(objAsKeyValuePair.Value);
            stream.WriteWithLengthPrefix(dataValue);
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

        public override SerializedType Type => SerializedType.KeyValuePair;
    }
}

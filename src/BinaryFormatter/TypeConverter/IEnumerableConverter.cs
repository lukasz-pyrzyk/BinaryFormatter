using System;
using BinaryFormatter.Types;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Reflection;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class IEnumerableConverter : BaseTypeConverter<object>
    {
        private int Size { get; set; }

        protected override void WriteObjectToStream(object obj, Stream stream)
        {
            var objectAsCollection = (IList)obj;

            byte[] collectionSize = BitConverter.GetBytes(objectAsCollection.Count);
            stream.Write(collectionSize);

            BinaryConverter converter = new BinaryConverter();
            foreach (var sourceElementValue in objectAsCollection)
            {
                if (sourceElementValue == null)
                    continue;

                object elementValue = (sourceElementValue as IEnumerable<object>);
                if (elementValue == null)
                {
                    elementValue = sourceElementValue;
                }
                else
                {
                    List<object> collectionOfObjects = new List<object>();
                    foreach (var item in (IList)elementValue)
                    {
                        collectionOfObjects.Add(item);
                    }
                    elementValue = collectionOfObjects;
                }

                byte[] data = converter.Serialize(elementValue);
                stream.WriteWithLengthPrefix(data);

                Size += data.Length;
            }
        }

        protected override object ProcessDeserialize(byte[] bytes, Type sourceType, ref int offset)
        {
            var stream = new WorkingStream(bytes);
            stream.ChangeOffset(offset);

            int beforeOffset = stream.Offset;
            List<object> deserializedCollection = null;

            if (bytes.Length > 0)
            {
                BinaryConverter converter = new BinaryConverter();
                deserializedCollection = new List<object>();

                int sizeCollection = stream.ReadInt();

                for (int i = 0; i < sizeCollection; i++)
                {
                    int sizeData = stream.ReadInt();
                    if (sizeData == 0)
                    {
                        continue;
                    }
                    byte[] dataValue = stream.ReadBytes(sizeData);

                    MethodInfo method = typeof(BinaryConverter).GetRuntimeMethod("Deserialize", new[] { typeof(byte[]) });
                    if (sourceType.GenericTypeArguments.Length > 0)
                    {
                        method = method.MakeGenericMethod(sourceType.GenericTypeArguments);
                    }
                    else
                    {
                        method = method.MakeGenericMethod(typeof(object));
                    }
                    object deserializeItem = method.Invoke(converter, new object[] { dataValue });

                    deserializedCollection.Add(deserializeItem);
                }
            }

            Size = stream.Offset - beforeOffset;
            stream.ChangeOffset(beforeOffset);

            return deserializedCollection;
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.IEnumerable;
    }
}

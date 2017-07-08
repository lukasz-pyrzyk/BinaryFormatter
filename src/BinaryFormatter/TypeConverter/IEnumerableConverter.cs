using System;
using BinaryFormatter.Types;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class IEnumerableConverter : BaseTypeConverter<object>
    {
        private int Size { get; set; }

        protected override void WriteObjectToStream(object obj, Stream stream)
        {
            var objAsIEnumerable = obj as IEnumerable<object>;
            if (objAsIEnumerable != null)
            {
                BinaryConverter converter = new BinaryConverter();
                foreach (var sourceElementValue in objAsIEnumerable)
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
                        foreach (var item in (IEnumerable<object>)elementValue)
                        {
                            collectionOfObjects.Add(item);
                        }

                        elementValue = collectionOfObjects;
                    }

                    Type elementType = elementValue.GetType();
                    byte[] typeInfo = Encoding.UTF8.GetBytes(elementType.AssemblyQualifiedName);
                    byte[] sizeTypeInfo = BitConverter.GetBytes(typeInfo.Length);
                    stream.Write(sizeTypeInfo);
                    stream.Write(typeInfo);

                    byte[] data = converter.Serialize(elementValue);
                    byte[] sizeData = BitConverter.GetBytes(data.Length);

                    stream.Write(sizeData);
                    stream.Write(data);

                    Size += data.Length;
                }
            }
        }

        protected override object ProcessDeserialize(byte[] stream, ref int offset)
        {
            List<object> deserializedCollection = null;

            if (stream.Length > 0)
            {
                BinaryConverter converter = new BinaryConverter();
                deserializedCollection = new List<object>();

                while (offset < stream.Length)
                {
                    int sizeTypeInfo = BitConverter.ToInt32(stream, offset);
                    offset += sizeof(int);
                    if (sizeTypeInfo == 0)
                    {
                        continue;
                    }

                    byte[] typeInfo = new byte[sizeTypeInfo];
                    Array.Copy(stream, offset, typeInfo, 0, sizeTypeInfo);
                    string typeFullName = Encoding.UTF8.GetString(typeInfo, 0, sizeTypeInfo);
                    Type valueType = System.Type.GetType(typeFullName);
                    offset += sizeTypeInfo;

                    int sizeData = BitConverter.ToInt32(stream, offset);
                    offset += sizeof(int);
                    if (sizeData == 0)
                    {
                        continue;
                    }
                    byte[] dataValue = new byte[sizeData];
                    Array.Copy(stream, offset, dataValue, 0, sizeData);
                    MethodInfo method = typeof(BinaryConverter).GetRuntimeMethod("Deserialize", new System.Type[] { typeof(byte[]) });
                    method = method.MakeGenericMethod(valueType);
                    object deserializeItem = method.Invoke(converter, new object[] { dataValue });
                    offset += sizeData;

                    deserializedCollection.Add(deserializeItem);
                }
            }

            return deserializedCollection;
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        public override SerializedType Type => SerializedType.IEnumerable;
    }
}

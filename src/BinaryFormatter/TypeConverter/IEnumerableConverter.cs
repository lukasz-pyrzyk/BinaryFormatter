using System;
using BinaryFormatter.Types;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace BinaryFormatter.TypeConverter
{
    internal class IEnumerableConverter : BaseTypeConverter<object>
    {
        private int Size { get; set; }

        protected override byte[] ProcessSerialize(object obj)
        {
            byte[] collectionAsByteArray = null;
            var objAsIEnumerable = (obj as IEnumerable<object>);
            if (objAsIEnumerable != null)
            {
                BinaryConverter converter = new BinaryConverter();
                List<byte> listAsArray = new List<byte>();
                foreach (var elementValue in objAsIEnumerable)
                {
                    if (elementValue == null)
                        continue;

                    Type elementType = elementValue.GetType();
                    byte[] typeInfo = Encoding.UTF8.GetBytes(elementType.AssemblyQualifiedName);
                    byte[] sizeTypeInfo = BitConverter.GetBytes(typeInfo.Length);

                    byte[] data = converter.Serialize(elementValue);
                    byte[] sizeData = BitConverter.GetBytes(data.Length);
                    int elementAsBytesLength =
                            sizeTypeInfo.Length +
                            typeInfo.Length +
                            sizeData.Length +
                            data.Length;

                    byte[] elementAsBytes = new byte[elementAsBytesLength];
                    Array.Copy(sizeTypeInfo, 0, elementAsBytes, 0, sizeTypeInfo.Length);
                    Array.Copy(typeInfo, 0, elementAsBytes, sizeTypeInfo.Length, typeInfo.Length);
                    Array.Copy(sizeData, 0, elementAsBytes, sizeTypeInfo.Length + typeInfo.Length, sizeData.Length);
                    Array.Copy(data, 0, elementAsBytes, sizeTypeInfo.Length + typeInfo.Length + sizeData.Length, data.Length);

                    listAsArray.AddRange(elementAsBytes);
                }

                collectionAsByteArray = listAsArray.ToArray();
                Size = collectionAsByteArray.Length;
            }

            return collectionAsByteArray;
        }

        protected override object ProcessDeserialize(byte[] stream, ref int offset)
        {
            List<object> deserializedCollection = new List<object>();

            if (stream.Length > 0)
            {
                BinaryConverter converter = new BinaryConverter();

                while(offset < stream.Length)
                {
                    int sizeTypeInfo = BitConverter.ToInt32(stream, offset);
                    offset += sizeof(int);
                    if(sizeTypeInfo == 0)
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

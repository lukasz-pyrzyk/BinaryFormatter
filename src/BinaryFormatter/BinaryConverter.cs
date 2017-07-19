using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BinaryFormatter.TypeConverter;
using BinaryFormatter.Types;
using System.Collections;
using System.IO;
using BinaryFormatter.Utils;
using System.Text;

namespace BinaryFormatter
{
    public class BinaryConverter
    {
        public byte[] Serialize(object obj)
        {
            var stream = new MemoryStream();
            Serialize(obj, stream);
            return stream.ToArray();
        }

        public void Serialize(object obj, Stream stream)
        {
            BaseTypeConverter converter = ConvertersSelector.SelectConverter(obj);            
            converter.Serialize(obj, stream);
        }

        public T Deserialize<T>(byte[] stream)
        {
            SerializedType deserializedType = (SerializedType)BitConverter.ToInt16(stream, 0);
            int offset = sizeof(short);

            if (deserializedType == SerializedType.Null)
            {
                object NullObject = null;
                return (T)NullObject;
            }

            int typeInfoSize = BitConverter.ToInt32(stream, offset);
            offset += sizeof(int);
            byte[] typeInfo = new byte[typeInfoSize];
            Array.Copy(stream, offset, typeInfo, 0, typeInfo.Length);
            string typeFullName = Encoding.UTF8.GetString(typeInfo, 0, typeInfo.Length);
            Type sourceType = System.Type.GetType(typeFullName);
            offset += typeInfoSize;

            BaseTypeConverter converter = ConvertersSelector.SelectConverter(sourceType);
            if (converter == null)
            {
                converter = ConvertersSelector.ForSerializedType(SerializedType.CustomObject);
            }

            if (converter is IEnumerableConverter)
            {
                var prepearedData = converter.DeserializeToObject(stream) as IEnumerable;

                var listType = typeof(List<>);
                var genericArgs = sourceType.GenericTypeArguments;
                var concreteType = listType.MakeGenericType(genericArgs);                
                var data = Activator.CreateInstance(concreteType);
                foreach (var item in prepearedData)
                {
                    ((IList)data).Add(item);
                }
                return (T)data;
            }

            return (T)converter.DeserializeToObject(stream);
        }
    }
}


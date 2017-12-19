using System;
using System.Collections.Generic;
using BinaryFormatter.TypeConverter;
using BinaryFormatter.Types;
using System.Collections;
using System.IO;
using BinaryFormatter.Utils;

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
            var workingStream = new WorkingStream(stream);

            SerializedType deserializedType = workingStream.ReadSerializedType();
            if (deserializedType == SerializedType.Null)
            {
                return default(T);
            }

            Type sourceType = deserializedType.GetBaseType();
            if (sourceType == null)
            {
                byte[] typeInfo = workingStream.ReadBytesWithSizePrefix();
                sourceType = TypeUtils.FromUTF8Bytes(typeInfo);
            }

            BaseTypeConverter converter = ConvertersSelector.SelectConverter(sourceType);
            if (converter is IEnumerableConverter)
            {
                var preparedData = converter.DeserializeToObject(stream) as IEnumerable;
                if (preparedData is IList)
                {
                    var listType = typeof(List<>);
                    var genericArgs = sourceType.GenericTypeArguments;
                    var concreteType = listType.MakeGenericType(genericArgs);
                    var data = Activator.CreateInstance(concreteType);
                    foreach (var item in preparedData)
                    {
                        ((IList)data).Add(item);
                    }
                    return (T)data;
                }

                return (T)preparedData;
            }

            return (T)converter.DeserializeToObject(stream);
        }
    }
}


using System;
using System.Collections.Generic;
using BinaryFormatter.TypeConverter;
using BinaryFormatter.Types;
using System.Collections;
using System.IO;
using BinaryFormatter.Streams;
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
            var serializationStream = new SerializationStream(stream);
            converter.Serialize(obj, serializationStream);
        }

        public T Deserialize<T>(byte[] bytes)
        {
            var stream = new DeserializationStream(bytes);

            SerializedType deserializedType = stream.ReadSerializedType();
            if (deserializedType == SerializedType.Null)
            {
                return default(T);
            }

            Type type = deserializedType.GetBaseType() ?? stream.ReadType();

            BaseTypeConverter converter = ConvertersSelector.SelectConverter(type);
            if (converter is IEnumerableConverter)
            {
                var preparedData = converter.Deserialize(stream, type) as IEnumerable;
                if (preparedData is IList)
                {
                    var listType = typeof(List<>);
                    var genericArgs = type.GenericTypeArguments;
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

            return (T)converter.Deserialize(stream, type);
        }
    }
}


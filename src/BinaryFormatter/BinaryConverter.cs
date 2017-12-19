﻿using System;
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

        public T Deserialize<T>(byte[] bytes)
        {
            var stream = new WorkingStream(bytes);

            SerializedType deserializedType = stream.ReadSerializedType();
            if (deserializedType == SerializedType.Null)
            {
                return default(T);
            }

            Type type = deserializedType.GetBaseType();
            if (type == null)
            {
                type = stream.ReadType();
            }

            BaseTypeConverter converter = ConvertersSelector.SelectConverter(type);
            if (converter is IEnumerableConverter)
            {
                var preparedData = converter.DeserializeToObject(stream, type) as IEnumerable;
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

            return (T)converter.DeserializeToObject(stream, type);
        }
    }
}


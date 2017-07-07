using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BinaryFormatter.TypeConverter;
using BinaryFormatter.Types;
using System.Collections;
using System.IO;
using BinaryFormatter.Utils;

namespace BinaryFormatter
{
    public class BinaryConverter
    {
        private static readonly List<string> excludedDlls = new List<string> { "CoreLib", "mscorlib" };
        private static readonly ConvertersSelector _selector = new ConvertersSelector();

        public byte[] Serialize(object obj)
        {
            var stream = new MemoryStream();
            Serialize(obj, stream);
            return stream.ToArray();
        }

        public void Serialize(object obj, Stream stream)
        {
            if (obj == null) return;

            BaseTypeConverter converter = _selector.SelectConverter(obj);
            if (converter != null)
            {
                byte[] serializedObject = converter.Serialize(obj);
                stream.Write(serializedObject);
            }
            else
            {
                SerializePropertiesToStream(obj, stream);
            }

        }

        private void SerializePropertiesToStream(object obj, Stream stream)
        {
            Type t = obj.GetType();
            ICollection<PropertyInfo> properties = t.GetTypeInfo().DeclaredProperties.ToArray();

            foreach (PropertyInfo property in properties)
            {
                object prop = property.GetValue(obj);
                byte[] serializedObject = Serialize(prop);
                stream.Write(serializedObject);
            }
        }

        public T Deserialize<T>(byte[] stream)
        {
            Type type = typeof(T);

            BaseTypeConverter converter = _selector.SelectConverter(type);
            if (converter == null)
            {
                T instance = (T)Activator.CreateInstance(type);

                int offset = 0;
                DeserializeObject(stream, ref instance, ref offset);

                return instance;
            }

            if (converter is IEnumerableConverter)
            {
                var prepearedData = converter.DeserializeToObject(stream) as IEnumerable;

                var listType = typeof(List<>);
                var genericArgs = type.GenericTypeArguments;
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

        private void DeserializeObject<T>(byte[] stream, ref T instance, ref int offset)
        {
            foreach (PropertyInfo property in instance.GetType().GetTypeInfo().DeclaredProperties)
            {
                DeserializeProperty(property, ref instance, stream, ref offset);
                if (offset == stream.Length)
                    return;
            }
        }

        private void DeserializeProperty<T>(PropertyInfo property, ref T instance, byte[] stream, ref int offset)
        {
            if (!excludedDlls.Any(x => property.PropertyType.AssemblyQualifiedName.Contains(x)))
            {
                object propertyValue = Activator.CreateInstance(property.PropertyType);
                property.SetValue(instance, propertyValue);
                DeserializeObject(stream, ref propertyValue, ref offset);
                return;
            }

            Type instanceType = typeof(T);
            TypeInfo instanceTypeInfo = instanceType.GetTypeInfo();
            SerializedType type = (SerializedType)BitConverter.ToInt16(stream, offset);
            offset += sizeof(short);

            BaseTypeConverter converter = _selector.ForSerializedType(type);
            object data;
            if (type == SerializedType.IEnumerable)
            {
                var prepearedData = converter.DeserializeToObject(stream, ref offset) as IEnumerable;

                var prop = property;
                var listType = typeof(List<>);
                var genericArgs = prop.PropertyType.GenericTypeArguments;
                var concreteType = listType.MakeGenericType(genericArgs);
                data = Activator.CreateInstance(concreteType);
                foreach (var item in prepearedData)
                {
                    ((IList)data).Add(item);
                }
            }
            else
            {
                data = converter.DeserializeToObject(stream, ref offset);
            }

            if (instanceTypeInfo.IsValueType && !instanceTypeInfo.IsPrimitive)
            {
                object boxedInstance = (object)instance;
                property.SetValue(boxedInstance, data, property.GetIndexParameters());
                instance = (T)boxedInstance;
            }
            else
            {
                property.SetValue(instance, data, property.GetIndexParameters());
            }
        }
    }
}


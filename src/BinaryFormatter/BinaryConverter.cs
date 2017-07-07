using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BinaryFormatter.TypeConverter;
using BinaryFormatter.Types;
using System.Collections;

namespace BinaryFormatter
{
    public class BinaryConverter
    {
        private static readonly List<string> excludedDlls = new List<string> { "CoreLib", "mscorlib" };
        private static readonly IDictionary<Type, BaseTypeConverter> _converters = new Dictionary<Type, BaseTypeConverter>
        {
            [typeof(byte)] = new ByteConverter(),
            [typeof(sbyte)] = new SByteConverter(),
            [typeof(char)] = new CharConverter(),
            [typeof(short)] = new ShortConverter(),
            [typeof(ushort)] = new UShortConverter(),
            [typeof(int)] = new IntConverter(),
            [typeof(uint)] = new UIntConverter(),
            [typeof(long)] = new LongConverter(),
            [typeof(ulong)] = new ULongConverter(),
            [typeof(float)] = new FloatConverter(),
            [typeof(double)] = new DoubleConverter(),
            [typeof(bool)] = new BoolConverter(),
            [typeof(decimal)] = new DecimalConverter(),
            [typeof(string)] = new StringConverter(),
            [typeof(DateTime)] = new DatetimeConverter(),
            [typeof(byte[])] = new ByteArrayConverter(),
            [typeof(IEnumerable)] = new IEnumerableConverter()
        };

        public byte[] Serialize(object obj)
        {
            Type t = obj.GetType();

            BaseTypeConverter converter;
            if (_converters.TryGetValue(t, out converter))
            {
                return converter.Serialize(obj);
            }
            if (obj is IEnumerable)
            {
                if (_converters.TryGetValue(typeof(IEnumerable), out converter))
                {
                    return converter.Serialize(obj);
                }
            }

            return SerializeProperties(obj);
        }

        private byte[] SerializeProperties(object obj)
        {
            Type t = obj.GetType();
            ICollection<PropertyInfo> properties = t.GetTypeInfo().DeclaredProperties.ToArray();

            List<byte> serializedObject = new List<byte>();
            foreach (PropertyInfo property in properties)
            {
                object prop = property.GetValue(obj);
                byte[] elementBytes = GetBytesFromProperty(prop);
                serializedObject.AddRange(elementBytes);
            }

            return serializedObject.ToArray();
        }

        private byte[] GetBytesFromProperty(object element)
        {
            if (element == null) return new byte[0];

            Type t = element.GetType();
            BaseTypeConverter converter;
            if (_converters.ContainsKey(t))
            {
                converter = _converters[t];
                return converter.Serialize(element);
            }
            if (element is IEnumerable)
            {
                if (_converters.TryGetValue(typeof(IEnumerable), out converter))
                {
                    return converter.Serialize(element);
                }
            }

            return SerializeProperties(element);
        }

        public T Deserialize<T>(byte[] stream)
        {
            Type type = typeof(T);

            bool isEnumerableType = type.GetTypeInfo().ImplementedInterfaces.Any(t => t == typeof(IEnumerable));

            BaseTypeConverter converter;
            if (_converters.TryGetValue(type, out converter))
            {
                return (T)converter.DeserializeToObject(stream);            
            }
            if (isEnumerableType)
            {
                if (_converters.TryGetValue(typeof(IEnumerable), out converter))
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
            }

            T instance = (T)Activator.CreateInstance(type);

            int offset = 0;
            DeserializeObject(stream, ref instance, ref offset);

            return instance;
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
            
            BaseTypeConverter converter = _converters.First(x => x.Value.Type == type).Value;
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
            } else
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


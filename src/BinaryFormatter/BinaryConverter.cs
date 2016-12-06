using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BinaryFormatter.TypeConverter;
using BinaryFormatter.Types;

namespace BinaryFormatter
{
    public class BinaryConverter
    {
        private readonly IDictionary<Type, BaseTypeConverter> _converters = new Dictionary<Type, BaseTypeConverter>
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
            [typeof(byte[])] = new ByteArrayConverter()
        };

        public byte[] Serialize(object obj)
        {
            Type t = obj.GetType();
            BaseTypeConverter converter;
            if (_converters.TryGetValue(t, out converter))
            {
                return converter.Serialize(obj);
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
            if (_converters.ContainsKey(t))
            {
                BaseTypeConverter converter = _converters[t];
                return converter.Serialize(element);
            }

            // TODO serialize if IEnumerable

            return SerializeProperties(element);
        }

        public T Deserialize<T>(byte[] stream)
        {
            BaseTypeConverter converter;
            if (_converters.TryGetValue(typeof(T), out converter))
            {
                return (T)converter.DeserializeToObject(stream);
            }

            T instance = (T)Activator.CreateInstance(typeof(T));

            int offset = 0;
            DeserializeObject(stream, instance, ref offset);

            return instance;
        }

        private void DeserializeObject<T>(byte[] stream, T instance, ref int offset)
        {
            foreach (PropertyInfo property in instance.GetType().GetTypeInfo().DeclaredProperties)
            {
                DeserializeProperty(property, instance, stream, ref offset);
                if (offset == stream.Length)
                    return;
            }
        }

        private void DeserializeProperty<T>(PropertyInfo property, T instance, byte[] stream, ref int offset)
        {
            if (!property.PropertyType.AssemblyQualifiedName.Contains("mscorlib"))
            {
                object propertyValue = Activator.CreateInstance(property.PropertyType);
                property.SetValue(instance, propertyValue);
                DeserializeObject(stream, propertyValue, ref offset);
                return;
            }

            SerializedType type = (SerializedType)BitConverter.ToInt16(stream, offset);
            offset += sizeof(short);

            BaseTypeConverter converter = _converters.First(x => x.Value.Type == type).Value;
            object data = converter.DeserializeToObject(stream, ref offset);
            property.SetValue(instance, data);
        }
    }
}

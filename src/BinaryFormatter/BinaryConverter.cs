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
            [typeof(DateTime)] = new DatetimeConverter()
        };

        public byte[] Serialize(object obj)
        {
            Type t = obj.GetType();

            ICollection<PropertyInfo> properties = t.GetProperties().ToArray();

            List<byte> serializedObject = new List<byte>();
            foreach (PropertyInfo property in properties)
            {
                object prop = property.GetValue(obj);
                byte[] elementBytes = GetBytesFromEement(prop);
                serializedObject.AddRange(elementBytes);
            }

            return serializedObject.ToArray();
        }

        public T Deserialize<T>(byte[] stream)
        {
            T instance = (T)Activator.CreateInstance(typeof(T));

            int offset = 0;
            foreach (PropertyInfo property in instance.GetType().GetProperties())
            {
                AssignValue(property, instance, stream, ref offset);
            }

            return instance;
        }

        private byte[] GetBytesFromEement(object element)
        {
            Type t = element.GetType();
            if (_converters.ContainsKey(t))
            {
                BaseTypeConverter converter = _converters[t];
                return converter.Serialize(element);
            }

            throw new InvalidOperationException("Cannot find specific BinaryConverter");
        }

        private void AssignValue<T>(PropertyInfo property, T instance, byte[] stream, ref int offset)
        {
            SerializedType type = (SerializedType)BitConverter.ToInt16(stream, offset);
            offset += sizeof(short);

            BaseTypeConverter converter = _converters.First(x => x.Value.Type == type).Value;
            object data = converter.DeserializeToObject(stream, ref offset);
            property.SetValue(instance, data);
        }
    }
}

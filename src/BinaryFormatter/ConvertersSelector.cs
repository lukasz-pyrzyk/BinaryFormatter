using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BinaryFormatter.TypeConverter;
using BinaryFormatter.Types;

namespace BinaryFormatter
{
    internal class ConvertersSelector
    {
        private static readonly Dictionary<Type, BaseTypeConverter> _converters = new Dictionary<Type, BaseTypeConverter>
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
            [typeof(IEnumerable)] = new IEnumerableConverter(),
            [typeof(object)] = new CustomObjectConverter()
        };
        private static readonly BaseTypeConverter NullConverter = new NullConverter();

        private ConvertersSelector()
        {
        }

        public static BaseTypeConverter SelectConverter(object obj)
        {
            if (obj == null) return NullConverter;

            Type type = obj.GetType();
            return SelectConverter(type);
        }

        public static BaseTypeConverter SelectConverter(Type type)
        {
            if (type == null) return NullConverter;

            BaseTypeConverter converter;
            if (_converters.TryGetValue(type, out converter))
            {
                return converter;
            }

            bool isEnumerableType = type.GetTypeInfo().ImplementedInterfaces.Any(t => t == typeof(IEnumerable));
            if (isEnumerableType)
            {
                if (_converters.TryGetValue(typeof(IEnumerable), out converter))
                {
                    return converter;
                }
            }

            return ForSerializedType(SerializedType.CustomObject);
        }

        public static BaseTypeConverter ForSerializedType(SerializedType type)
        {
            if (type == SerializedType.Null)
                return NullConverter;

            return _converters.First(x => x.Value.Type == type).Value;
        }
    }
}

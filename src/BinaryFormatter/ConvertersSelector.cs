using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BinaryFormatter.TypeConverter;
using BinaryFormatter.Types;
using System.Numerics;

namespace BinaryFormatter
{
    internal static class ConvertersSelector
    {
        private static readonly Dictionary<Type, BaseTypeConverter> Converters = new Dictionary<Type, BaseTypeConverter>
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
            [typeof(TimeSpan)] = new TimespanConverter(),
            [typeof(byte[])] = new ByteArrayConverter(),
            [typeof(IEnumerable)] = new IEnumerableConverter(),
            [typeof(object)] = new CustomObjectConverter(),
            [typeof(Guid)] = new GuidConverter(),
            [typeof(Uri)] = new UriConverter(),
            [typeof(Enum)] = new EnumConverter(),
            [typeof(KeyValuePair<,>)] = new KeyValuePairConverter(),
            [typeof(BigInteger)] = new BigIntegerConverter()
        };

        private static readonly Dictionary<SerializedType, BaseTypeConverter> ConvertersBySerializedType =
            new Dictionary<SerializedType, BaseTypeConverter>();

        static ConvertersSelector()
        {
            ConvertersBySerializedType.Add(SerializedType.Null, NullConverter);

            foreach (var pair in Converters)
            {
                ConvertersBySerializedType.Add(pair.Value.Type, pair.Value);
            }
        }

        private static readonly BaseTypeConverter NullConverter = new NullConverter();

        public static BaseTypeConverter SelectConverter(object obj)
        {
            if (obj == null) return NullConverter;

            Type type = obj.GetType();
            return SelectConverter(type);
        }

        public static BaseTypeConverter SelectConverter(Type type)
        {
            if (type == null) return NullConverter;

            if (Converters.TryGetValue(type, out BaseTypeConverter converter))
            {
                return converter;
            }

            TypeInfo typeInfo = type.GetTypeInfo();
            bool isKeyValuePair = typeInfo.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
            if (isKeyValuePair)
            {
                if (Converters.TryGetValue(typeof(KeyValuePair<,>), out converter))
                {
                    return converter;
                }
            }

            bool isEnumerableType = type.GetTypeInfo().ImplementedInterfaces.Any(t => t == typeof(IEnumerable));
            if (isEnumerableType)
            {
                if (Converters.TryGetValue(typeof(IEnumerable), out converter))
                {
                    return converter;
                }
            }

            bool isEnumType = type.GetTypeInfo().IsEnum;
            if (isEnumType)
            {
                if (Converters.TryGetValue(typeof(Enum), out converter))
                {
                    return converter;
                }
            }

            return ForSerializedType(SerializedType.CustomObject);
        }

        public static BaseTypeConverter ForSerializedType(SerializedType type)
        {
            if (ConvertersBySerializedType.TryGetValue(type, out BaseTypeConverter converter))
            {
                return converter;
            }

            throw new SerializationException($"Unable to find find a converter for type {type}");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BinaryFormatter.TypeConverter;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter
{
    internal static class ConvertersSelector
    {
        private static readonly Dictionary<SerializedType, BaseTypeConverter> Converters = new Dictionary<SerializedType, BaseTypeConverter>()
        {
            [SerializedType.Null] = new NullConverter(),
            [SerializedType.Byte] = new ByteConverter(),
            [SerializedType.Sbyte] = new SByteConverter(),
            [SerializedType.Char] = new CharConverter(),
            [SerializedType.Short] = new ShortConverter(),
            [SerializedType.UShort] = new UShortConverter(),
            [SerializedType.Int] = new IntConverter(),
            [SerializedType.Uint] = new UIntConverter(),
            [SerializedType.Long] = new LongConverter(),
            [SerializedType.Ulong] = new ULongConverter(),
            [SerializedType.Float] = new FloatConverter(),
            [SerializedType.Double] = new DoubleConverter(),
            [SerializedType.Bool] = new BoolConverter(),
            [SerializedType.Decimal] = new DecimalConverter(),
            [SerializedType.String] = new StringConverter(),
            [SerializedType.Datetime] = new DatetimeConverter(),
            [SerializedType.Timespan] = new TimespanConverter(),
            [SerializedType.ByteArray] = new ByteArrayConverter(),
            [SerializedType.IEnumerable] = new IEnumerableConverter(),
            [SerializedType.KeyValuePair] = new KeyValuePairConverter(),
            [SerializedType.Guid] = new GuidConverter(),
            [SerializedType.Uri] = new UriConverter(),
            [SerializedType.Enum] = new EnumConverter(),
            [SerializedType.BigInteger] = new BigIntegerConverter(),
            [SerializedType.CustomObject] = new CustomObjectConverter(),
        };

        public static BaseTypeConverter SelectConverter(object obj)
        {
            if (obj is null) return Converters[SerializedType.Null];

            Type type = obj.GetType();
            return SelectConverter(type);
        }

        public static BaseTypeConverter SelectConverter(Type type)
        {
            if (type is null) return Converters[SerializedType.Null];

            if (Converters.TryGetValue(type.GetSerializedType(), out BaseTypeConverter converter))
            {
                return converter;
            }

            TypeInfo typeInfo = type.GetTypeInfo();
            bool isKeyValuePair = typeInfo.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
            if (isKeyValuePair)
            {
                if (Converters.TryGetValue(SerializedType.KeyValuePair, out converter))
                {
                    return converter;
                }
            }

            bool isEnumerableType = type.GetTypeInfo().ImplementedInterfaces.Any(t => t == typeof(IEnumerable));
            if (isEnumerableType)
            {
                if (Converters.TryGetValue(SerializedType.IEnumerable, out converter))
                {
                    return converter;
                }
            }

            bool isEnumType = type.GetTypeInfo().IsEnum;
            if (isEnumType)
            {
                if (Converters.TryGetValue(SerializedType.Enum, out converter))
                {
                    return converter;
                }
            }

            return ForSerializedType(SerializedType.CustomObject);
        }

        public static BaseTypeConverter ForSerializedType(SerializedType type)
        {
            if (Converters.TryGetValue(type, out BaseTypeConverter converter))
            {
                return converter;
            }

            throw new SerializationException($"Unable to find find a converter for type {type}");
        }
    }
}

﻿using System;
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

        private static readonly Dictionary<SerializedType, BaseTypeConverter> _convertersBySerializedType =
            new Dictionary<SerializedType, BaseTypeConverter>();

        static ConvertersSelector()
        {
            _convertersBySerializedType.Add(SerializedType.Null, NullConverter);

            foreach (var pair in _converters)
            {
                _convertersBySerializedType.Add(pair.Value.Type, pair.Value);
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

            BaseTypeConverter converter;
            if (_converters.TryGetValue(type, out converter))
            {
                return converter;
            }

            TypeInfo typeInfo = type.GetTypeInfo();
            bool isKeyValuePair = typeInfo.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
            if (isKeyValuePair)
            {
                if (_converters.TryGetValue(typeof(KeyValuePair<,>), out converter))
                {
                    return converter;
                }
            }

            bool isEnumerableType = type.GetTypeInfo().ImplementedInterfaces.Any(t => t == typeof(IEnumerable));
            if (isEnumerableType)
            {
                if (_converters.TryGetValue(typeof(IEnumerable), out converter))
                {
                    return converter;
                }
            }

            bool isEnumType = type.GetTypeInfo().IsEnum;
            if (isEnumType)
            {
                if (_converters.TryGetValue(typeof(Enum), out converter))
                {
                    return converter;
                }
            }

            return ForSerializedType(SerializedType.CustomObject);
        }

        public static BaseTypeConverter ForSerializedType(SerializedType type)
        {
            BaseTypeConverter converter;
            if (_convertersBySerializedType.TryGetValue(type, out converter))
            {
                return converter;
            }

            throw new SerializationException($"Unable to find find a converter for type {type}");
        }
    }
}

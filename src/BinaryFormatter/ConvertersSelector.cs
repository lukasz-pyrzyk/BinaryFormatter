using System;
using System.Collections;
using System.Collections.Generic;
using BinaryFormatter.TypeConverter;

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
            [typeof(IEnumerable)] = new IEnumerableConverter()
        };

        public BaseTypeConverter SelectConverter(object obj)
        {
            if(obj == null) return null;
            Type t = obj.GetType();

            BaseTypeConverter converter;
            if (_converters.TryGetValue(t, out converter))
            {
                return converter;
            }

            if (obj is IEnumerable)
            {
                if (_converters.TryGetValue(typeof(IEnumerable), out converter))
                {
                    return converter;
                }
            }

            return null;
        }
    }
}

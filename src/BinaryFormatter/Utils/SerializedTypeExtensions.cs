using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BinaryFormatter.Types;
using System.Numerics;
using System.Reflection;

namespace BinaryFormatter.Utils
{
    public static class SerializedTypeExtensions
    {
        internal static Type GetBaseType(this SerializedType serializedType)
        {
            if (serializedType == SerializedType.Bool) return typeof(bool);
            if (serializedType == SerializedType.Byte) return typeof(byte);
            if (serializedType == SerializedType.ByteArray) return typeof(byte[]);
            if (serializedType == SerializedType.Char) return typeof(char);
            if (serializedType == SerializedType.Datetime) return typeof(DateTime);
            if (serializedType == SerializedType.Timespan) return typeof(TimeSpan);
            if (serializedType == SerializedType.Decimal) return typeof(decimal);
            if (serializedType == SerializedType.Double) return typeof(double);
            if (serializedType == SerializedType.Float) return typeof(float);
            if (serializedType == SerializedType.Int) return typeof(int);
            if (serializedType == SerializedType.Long) return typeof(long);
            if (serializedType == SerializedType.Sbyte) return typeof(sbyte);
            if (serializedType == SerializedType.Short) return typeof(short);
            if (serializedType == SerializedType.String) return typeof(string);
            if (serializedType == SerializedType.Uint) return typeof(uint);
            if (serializedType == SerializedType.Ulong) return typeof(ulong);
            if (serializedType == SerializedType.UShort) return typeof(ushort);
            if (serializedType == SerializedType.Guid) return typeof(Guid);
            if (serializedType == SerializedType.Uri) return typeof(Uri);
            if (serializedType == SerializedType.BigInteger) return typeof(BigInteger);

            return null;
        }

        internal static SerializedType GetSerializedType(this Type type)
        {
            if (type is null) return SerializedType.Null;
            if (type == typeof(bool)) return SerializedType.Bool;
            if (type == typeof(byte)) return SerializedType.Byte;
            if (type == typeof(byte[])) return SerializedType.ByteArray;
            if (type == typeof(char)) return SerializedType.Char;
            if (type == typeof(DateTime)) return SerializedType.Datetime;
            if (type == typeof(TimeSpan)) return SerializedType.Timespan;
            if (type == typeof(decimal)) return SerializedType.Decimal;
            if (type == typeof(double)) return SerializedType.Double;
            if (type == typeof(float)) return SerializedType.Float;
            if (type == typeof(int)) return SerializedType.Int;
            if (type == typeof(long)) return SerializedType.Long;
            if (type == typeof(sbyte)) return SerializedType.Sbyte;
            if (type == typeof(short)) return SerializedType.Short;
            if (type == typeof(string)) return SerializedType.String;
            if (type == typeof(uint)) return SerializedType.Uint;
            if (type == typeof(ulong)) return SerializedType.Ulong;
            if (type == typeof(ushort)) return SerializedType.UShort;
            if (type == typeof(Guid)) return SerializedType.Guid;
            if (type == typeof(Uri)) return SerializedType.Uri;
            if (type == typeof(BigInteger)) return SerializedType.BigInteger;

            TypeInfo typeInfo = type.GetTypeInfo();
            bool isKeyValuePair = typeInfo.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
            if (isKeyValuePair)
            {
                return SerializedType.KeyValuePair;
            }

            bool isEnumType = type.GetTypeInfo().IsEnum;
            if (isEnumType)
            {
                return SerializedType.Enum;
            }

            return SerializedType.Unknown;
        }
    }
}

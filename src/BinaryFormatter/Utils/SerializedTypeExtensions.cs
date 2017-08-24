using System;
using BinaryFormatter.Types;
using System.Collections.Generic;
using System.Numerics;

namespace BinaryFormatter.Utils
{
    public static class SerializedTypeExtensions
    {
        internal static SerializedType ReadSerializedType(this byte[] bytes, ref int offset)
        {
            short type = BitConverter.ToInt16(bytes, offset);
            offset += sizeof(short);
            return (SerializedType)type;
        }

        internal static bool IsBaseType(this SerializedType serializedType)
        {
            return serializedType.GetBaseType() != null;
        }

        internal static Type GetBaseType(this SerializedType serializedType)
        {
            switch (serializedType)
            {
                case SerializedType.Bool:
                    return typeof(bool);
                case SerializedType.Byte:
                    return typeof(byte);
                case SerializedType.ByteArray:
                    return typeof(byte[]);
                case SerializedType.Char:
                    return typeof(char);
                case SerializedType.Datetime:
                    return typeof(DateTime);
                case SerializedType.Decimal:
                    return typeof(decimal);
                case SerializedType.Double:
                    return typeof(double);
                case SerializedType.Float:
                    return typeof(float);
                case SerializedType.Int:
                    return typeof(int);
                case SerializedType.Long:
                    return typeof(long);
                case SerializedType.Sbyte:
                    return typeof(sbyte);
                case SerializedType.Short:
                    return typeof(short);
                case SerializedType.String:
                    return typeof(string);
                case SerializedType.Uint:
                    return typeof(uint);
                case SerializedType.Ulong:
                    return typeof(ulong);
                case SerializedType.UShort:
                    return typeof(ushort);
                case SerializedType.Guid:
                    return typeof(Guid);
                case SerializedType.Uri:
                    return typeof(Uri);
                case SerializedType.BitInteger:
                    return typeof(BigInteger);
                default:
                    return null;
            }
        }
    }
}

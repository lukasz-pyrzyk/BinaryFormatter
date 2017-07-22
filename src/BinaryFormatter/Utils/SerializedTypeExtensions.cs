using System;
using BinaryFormatter.Types;

namespace BinaryFormatter.Utils
{
    public static class SerializedTypeExtensions
    {
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
                default:
                    return null;
            }
        }
    }
}

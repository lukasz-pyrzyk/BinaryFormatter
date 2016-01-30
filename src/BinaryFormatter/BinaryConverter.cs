using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BinaryFormatter.TypeConverter;

namespace BinaryFormatter
{
    public class BinaryConverter
    {
        public byte[] Parse(object obj)
        {
            Type t = obj.GetType();

            ICollection<PropertyInfo> properties = t.GetProperties().ToArray();
            int totalSize = 150 + sizeof(int) * properties.Count;

            byte[] array = new byte[totalSize];

            int offset = 0;
            foreach (PropertyInfo property in t.GetProperties())
            {
                object prop = property.GetValue(obj);
                Byte[] elementBytes = GetBytesFromEement(prop);

                Array.ConstrainedCopy(elementBytes, 0, array, offset, elementBytes.Length);
                offset += elementBytes.Length;
            }

            return new byte[0];
        }

        private static byte[] GetBytesFromEement(object element)
        {
            if (element is byte)
            {
                return new ByteConverter().Serialize(element);
            }
            if (element is sbyte)
            {
                return new SByteConverter().Serialize(element);
            }
            if (element is int)
            {
                return new IntConverter().Serialize(element);
            }
            if (element is uint)
            {
                return new UIntConverter().Serialize(element);
            }
            if (element is short)
            {
                return new ShortConverter().Serialize(element);
            }
            if (element is ushort)
            {
                return new UShortConverter().Serialize(element);
            }
            if (element is long)
            {
                return new LongConverter().Serialize(element);
            }
            if (element is ulong)
            {
                return new ULongConverter().Serialize(element);
            }
            if (element is float)
            {
                return new FloatConverter().Serialize(element);
            }
            if (element is double)
            {
                return new DoubleConverter().Serialize(element);
            }
            if (element is char)
            {
                return new CharConverter().Serialize(element);
            }
            if (element is bool)
            {
                return new BoolConverter().Serialize(element);
            }
            if (element is string)
            {
                return new StringConverter().Serialize(element);
            }
            if (element is decimal)
            {
                return new DecimalConverter().Serialize(element);
            }

            throw new InvalidOperationException("Cannot find specific BinaryConverter");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BinaryFormatter.TypeConverter;

namespace BinaryFormatter
{
    public class BinaryConverter
    {
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

        private void AssignValue<T>(PropertyInfo property, T instance, byte[] stream, ref int offset)
        {
            int size = BitConverter.ToInt32(stream, offset);
            offset += sizeof (int);

        }

        private byte[] GetBytesFromEement(object element)
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
            if (element != null && element.GetType().Name != "System.Object")
            {
                return Serialize(element);
            }

            throw new InvalidOperationException("Cannot find specific BinaryConverter");
        }
    }
}

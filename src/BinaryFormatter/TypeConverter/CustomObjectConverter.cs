using System;
using BinaryFormatter.Types;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Linq;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class CustomObjectConverter : BaseTypeConverter<object>
    {
        private static readonly List<string> excludedDlls = new List<string> { "CoreLib", "mscorlib" };
        private int Size { get; set; }

        protected override void WriteObjectToStream(object obj, Stream stream)
        {
            Type t = obj.GetType();

            ICollection<PropertyInfo> properties = t.GetTypeInfo().GetAllProperties().ToArray();

            foreach (PropertyInfo property in properties)
            {
                if (!property.CanWrite || property.GetMethod.IsStatic)
                    continue;

                object prop = property.GetValue(obj);
                var converter = ConvertersSelector.SelectConverter(prop);
                converter.Serialize(prop, stream);
            }
        }

        protected override object ProcessDeserialize(byte[] bytes, Type sourceType, ref int offset)
        {
            var instance = Activator.CreateInstance(sourceType);

            foreach (PropertyInfo property in sourceType.GetTypeInfo().GetAllProperties())
            {
                if (!property.CanWrite)
                    continue;

                DeserializeProperty(property, ref instance, bytes, ref offset);
                if (offset == bytes.Length)
                    break;
            }

            return instance;
        }

        protected override int GetTypeSize()
        {
            return Size;
        }

        private void DeserializeProperty<T>(PropertyInfo property, ref T instance, byte[] stream, ref int offset)
        {
            Type instanceType = property.PropertyType;
            TypeInfo instanceTypeInfo = instanceType.GetTypeInfo();
            SerializedType type = stream.ReadSerializedType(ref offset);

            if (!excludedDlls.Any(x => property.PropertyType.AssemblyQualifiedName.Contains(x)))
            {
                if (type == SerializedType.Null)
                {
                    property.SetValue(instance, null);
                }
                else if (type == SerializedType.Enum)
                {
                    object propertyValue = Activator.CreateInstance(property.PropertyType);
                    DeserializeEnum(stream, ref propertyValue, ref offset);
                    property.SetValue(instance, propertyValue);
                }
                else
                {
                    object propertyValue = Activator.CreateInstance(property.PropertyType);
                    DeserializeObject(stream, ref propertyValue, ref offset);
                    property.SetValue(instance, propertyValue);
                }
                return;
            }

            if (type == SerializedType.Null)
            {
                property.SetValue(instance, null, property.GetIndexParameters());
                return;
            }

            if (!property.PropertyType.GetTypeInfo().IsBaseType())
            {
                int typeInfoSize = BitConverter.ToInt32(stream, offset);
                offset += sizeof(int);
                offset += typeInfoSize;
            }

            BaseTypeConverter converter = ConvertersSelector.ForSerializedType(type);
            object data;
            if (type == SerializedType.Null)
            {
                data = null;
            }
            else if (type == SerializedType.IEnumerable)
            {
                var preparedData = converter.DeserializeToObject(stream, ref offset) as IEnumerable;

                var prop = property;
                var listType = typeof(List<>);
                var genericArgs = prop.PropertyType.GenericTypeArguments;
                var concreteType = listType.MakeGenericType(genericArgs);
                data = Activator.CreateInstance(concreteType);
                foreach (var item in preparedData)
                {
                    ((IList)data).Add(item);
                }
            }
            else
            {
                data = converter.DeserializeToObject(stream, ref offset);
            }

            if (instanceTypeInfo.IsValueType && !instanceTypeInfo.IsPrimitive)
            {
                object boxedInstance = instance;
                property.SetValue(boxedInstance, data, property.GetIndexParameters());
                instance = (T)boxedInstance;
            }
            else
            {
                property.SetValue(instance, data, property.GetIndexParameters());
            }
        }

        private void DeserializeEnum(byte[] stream, ref object propertyValue, ref int offset)
        {
            var ws = new WorkingStream(stream, offset);
            Type type = ws.ReadType();

            var converter = new EnumConverter();
            propertyValue = converter.DeserializeInto(ws, type);

            offset = ws.Offset;
        }

        private void DeserializeObject<T>(byte[] stream, ref T instance, ref int offset)
        {
            int typeInfoSize = BitConverter.ToInt32(stream, offset);
            offset += sizeof(int);
            offset += typeInfoSize;

            foreach (PropertyInfo property in instance.GetType().GetTypeInfo().GetAllProperties())
            {
                if (!property.CanWrite)
                    continue;

                DeserializeProperty(property, ref instance, stream, ref offset);
                if (offset == stream.Length)
                    return;
            }
        }

        public override SerializedType Type => SerializedType.CustomObject;
    }
}

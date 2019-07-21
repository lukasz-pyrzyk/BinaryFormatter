using System;
using BinaryFormatter.Types;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;
using BinaryFormatter.Streams;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal class CustomObjectConverter : BaseTypeConverter<object>
    {
        private static readonly List<string> ExcludedDlls = new List<string> { "CoreLib", "mscorlib" };

        protected override void SerializeInternal(object obj, SerializationStream stream)
        {
            var fields = obj.GetType().GetFieldsAccessibleForSerializer();

            foreach (var field in fields)
            {
                object prop = field.GetValue(obj);
                var converter = ConvertersSelector.SelectConverter(prop);
                converter.Serialize(prop, stream);
            }
        }

        protected override object DeserializeInternal(DeserializationStream stream, Type sourceType)
        {
            var instance = Activator.CreateInstance(sourceType);

            foreach (var field in sourceType.GetFieldsAccessibleForSerializer())
            {
                DeserializeField(field, ref instance, stream);
                if (stream.HasEnded)
                    break;
            }

            return instance;
        }

        private void DeserializeField<T>(FieldInfo field, ref T instance, DeserializationStream stream)
        {
            Type instanceType = field.FieldType;
            TypeInfo instanceTypeInfo = instanceType.GetTypeInfo();
            SerializedType type = stream.ReadSerializedType();

            if (!ExcludedDlls.Any(x => field.FieldType.AssemblyQualifiedName.Contains(x)))
            {
                if (type == SerializedType.Null)
                {
                    field.SetValue(instance, null);
                }
                else if (type == SerializedType.Enum)
                {
                    object propertyValue = Activator.CreateInstance(field.FieldType);
                    DeserializeEnum(stream, ref propertyValue);
                    field.SetValue(instance, propertyValue);
                }
                else
                {
                    object propertyValue = Activator.CreateInstance(field.FieldType);
                    DeserializeObject(stream, ref propertyValue);
                    field.SetValue(instance, propertyValue);
                }
                return;
            }

            if (type == SerializedType.Null)
            {
                field.SetValue(instance, null);
                return;
            }

            if (!field.FieldType.GetTypeInfo().IsBaseType())
            {
                stream.ReadType();
            }

            BaseTypeConverter converter = ConvertersSelector.ForSerializedType(type);
            object data;
            if (type == SerializedType.Null)
            {
                data = null;
            }
            else if (type == SerializedType.IEnumerable)
            {
                var preparedData = converter.Deserialize(stream) as IEnumerable;

                var prop = field;
                var listType = typeof(List<>);
                var genericArgs = prop.FieldType.GenericTypeArguments;
                var concreteType = listType.MakeGenericType(genericArgs);
                data = Activator.CreateInstance(concreteType);
                foreach (var item in preparedData)
                {
                    ((IList)data).Add(item);
                }
            }
            else
            {
                data = converter.Deserialize(stream);
            }

            if (instanceTypeInfo.IsValueType && !instanceTypeInfo.IsPrimitive)
            {
                object boxedInstance = instance;
                field.SetValue(boxedInstance, data);
                instance = (T)boxedInstance;
            }
            else
            {
                field.SetValue(instance, data);
            }
        }

        private void DeserializeEnum(DeserializationStream stream, ref object propertyValue)
        {
            Type type = stream.ReadType();
            var converter = new EnumConverter();
            propertyValue = converter.DeserializeInto(stream, type);
        }

        private void DeserializeObject<T>(DeserializationStream stream, ref T instance)
        {
            stream.ReadType();

            foreach (var field in instance.GetType().GetFieldsAccessibleForSerializer())
            {
                DeserializeField(field, ref instance, stream);
                if (stream.HasEnded)
                    return;
            }
        }

        public override SerializedType Type => SerializedType.CustomObject;
    }
}

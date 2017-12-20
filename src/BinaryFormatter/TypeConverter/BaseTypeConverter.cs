using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;
using System.Text;
using System.Reflection;
using BinaryFormatter.Streams;

namespace BinaryFormatter.TypeConverter
{
    internal abstract class BaseTypeConverter<T> : BaseTypeConverter
    {
        public override void Serialize(object obj, Stream stream)
        {
            Serialize((T)obj, stream);
        }

        public void Serialize(T obj, Stream stream)
        {
            Type destinationType = typeof(T);
            byte[] objectType = BitConverter.GetBytes((ushort)Type);
            stream.Write(objectType);

            if (obj != null)
            {
                if (!destinationType.GetTypeInfo().IsBaseType())
                {
                    byte[] typeInfo = Encoding.UTF8.GetBytes(obj.GetType().AssemblyQualifiedName);
                    stream.WriteWithLengthPrefix(typeInfo);
                }

                SerializeInternal(obj, stream);
            }
        }

        protected abstract void SerializeInternal(T obj, Stream stream);

        public override object Deserialize(DeserializationStream stream)
        {
            return Deserialize(stream, typeof(T));
        }

        public override object Deserialize(DeserializationStream stream, Type type)
        {
            return DeserializeInternal(stream, type);
        }

        protected abstract T DeserializeInternal(DeserializationStream stream, Type sourceType);
    }

    internal abstract class BaseTypeConverter
    {
        public abstract void Serialize(object obj, Stream stream);
        public abstract object Deserialize(DeserializationStream stream);
        public abstract object Deserialize(DeserializationStream stream, Type type);
        public abstract SerializedType Type { get; }
    }
}
using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;
using System.Text;
using System.Reflection;

namespace BinaryFormatter.TypeConverter
{
    internal abstract class BaseTypeConverter<T> : BaseTypeConverter
    {
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

                WriteObjectToStream(obj, stream);
            }
        }

        public override void Serialize(object obj, Stream stream)
        {
            Serialize((T)obj, stream);
        }

        public override object DeserializeToObject(WorkingStream stream)
        {
            return DeserializeToObject(stream, typeof(T));
        }

        public override object DeserializeToObject(WorkingStream stream, Type type)
        {
            return ProcessDeserialize(stream, type);
        }

        protected abstract void WriteObjectToStream(T obj, Stream stream);
        protected abstract T ProcessDeserialize(WorkingStream stream, Type sourceType);
    }

    internal abstract class BaseTypeConverter
    {
        public abstract void Serialize(object obj, Stream stream);
        public abstract object DeserializeToObject(WorkingStream stream);
        public abstract object DeserializeToObject(WorkingStream stream, Type type);
        public abstract SerializedType Type { get; }
    }
}
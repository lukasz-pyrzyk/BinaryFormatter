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
            Type destinationnType = typeof(T);
            byte[] objectType = BitConverter.GetBytes((ushort)Type);
            stream.Write(objectType);

            if (obj != null)
            {
                if (!destinationnType.GetTypeInfo().IsBaseType())
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

        public T Deserialize(byte[] bytes)
        {
            var stream = new WorkingStream(bytes);
            SerializedType deserializedType = stream.ReadSerializedType();
            Type sourceType = deserializedType.GetBaseType();

            if (sourceType == null)
            {
                byte[] typeInfo = stream.ReadBytesWithSizePrefix();
                sourceType = TypeUtils.FromUTF8Bytes(typeInfo);
            }

            int offset = stream.Offset;
            return ProcessDeserialize(bytes, sourceType, ref offset);
        }

        public T Deserialize(byte[] bytes, ref int offset)
        {
            T obj = ProcessDeserialize(bytes, typeof(T), ref offset);
            offset += GetTypeSize();
            return obj;
        }

        public override object DeserializeToObject(byte[] stream)
        {
            return Deserialize(stream);
        }

        public override object DeserializeToObject(byte[] stream, ref int offset)
        {
            return Deserialize(stream, ref offset);
        }

        protected abstract int GetTypeSize();
        protected abstract void WriteObjectToStream(T obj, Stream stream);
        protected abstract T ProcessDeserialize(byte[] bytes, Type sourceType, ref int offset);
    }

    internal abstract class BaseTypeConverter
    {
        public abstract void Serialize(object obj, Stream stream);
        public abstract object DeserializeToObject(byte[] stream);
        public abstract object DeserializeToObject(byte[] stream, ref int offset);
        public abstract SerializedType Type { get; }
    }
}
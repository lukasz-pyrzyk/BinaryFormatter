using System;
using System.IO;
using BinaryFormatter.Types;
using BinaryFormatter.Utils;

namespace BinaryFormatter.TypeConverter
{
    internal abstract class BaseTypeConverter<T> : BaseTypeConverter
    {
        public void Serialize(T obj, Stream stream)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            byte[] objectBytes = ProcessSerialize(obj);
            byte[] objectType = BitConverter.GetBytes((ushort)Type);

            stream.Write(objectType);
            stream.Write(objectBytes);
        }

        public override void Serialize(object obj, Stream stream)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            Serialize((T)obj, stream);
        }

        public T Deserialize(byte[] stream)
        {
            int offset = sizeof (short);
            return ProcessDeserialize(stream, ref offset);
        }

        public T Deserialize(byte[] stream, ref int offset)
        {
            T obj = ProcessDeserialize(stream, ref offset);
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
        protected abstract byte[] ProcessSerialize(T obj);
        protected abstract T ProcessDeserialize(byte[] stream, ref int offset);

        protected virtual SerializedType GetPackageType(byte[] stream, ref int offset)
        {
            short type = BitConverter.ToInt16(stream, offset);
            offset += sizeof (short);
            return (SerializedType)type;
        }
    }

    internal abstract class BaseTypeConverter
    {
        public abstract void Serialize(object obj, Stream stream);
        public abstract object DeserializeToObject(byte[] stream);
        public abstract object DeserializeToObject(byte[] stream, ref int offset);
        public abstract SerializedType Type { get; }
    }
}
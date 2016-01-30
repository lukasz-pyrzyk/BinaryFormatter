using System;
using System.Runtime.InteropServices;

namespace BinaryFormatter.TypeConverter
{
    internal abstract class BaseTypeConverter<T> : BaseTypeConverter
    {
        public override byte[] Serialize(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            return Serialize((T)obj);
        }

        public byte[] Serialize(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            byte[] objectBytes = ProcessSerialize(obj);
            byte[] sizeBytes = BitConverter.GetBytes(GetTypeSize());

            byte[] final = new byte[sizeBytes.Length + objectBytes.Length];

            Array.Copy(sizeBytes, 0, final, 0, sizeBytes.Length);
            Array.Copy(objectBytes, 0, final, sizeBytes.Length, objectBytes.Length);

            return final;
        }

        protected virtual int GetTypeSize()
        {
            return Marshal.SizeOf<T>();
        }

        protected abstract byte[] ProcessSerialize(T obj);
    }

    internal abstract class BaseTypeConverter
    {
        public abstract byte[] Serialize(object obj);
    }
}
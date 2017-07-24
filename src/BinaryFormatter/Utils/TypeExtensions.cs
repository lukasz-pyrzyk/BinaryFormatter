using System;
using System.Text;

namespace BinaryFormatter.Utils
{
    public static class TypeUtils
    {
        public static Type FromUTF8Bytes(byte[] bytes)
        {
            string typeFullName = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            return Type.GetType(typeFullName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BinaryFormatter.Utils
{
    public static class TypeHelper
    {
        public static KeyValuePair<object, object> CastFrom(Object obj)
        {
            var type = obj.GetType();
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsGenericType)
            {
                if (type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                {
                    var key = type.GetRuntimeProperty("Key");
                    var value = type.GetRuntimeProperty("Value");
                    var keyObj = key.GetValue(obj, null);
                    var valueObj = value.GetValue(obj, null);
                    return new KeyValuePair<object, object>(keyObj, valueObj);
                }
            }
            throw new ArgumentException(" ### -> public static KeyValuePair<object , object > CastFrom(Object obj): Error: obj argument must be KeyValuePair <,> ");
        }
    }
}

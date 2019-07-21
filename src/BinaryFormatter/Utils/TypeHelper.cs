using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BinaryFormatter.Types;

namespace BinaryFormatter.Utils
{
    internal static class TypeHelper
    {
        internal static KeyValuePair<object, object> CastFrom(object obj)
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

        internal static bool IsDictionary(object obj)
        {
            return obj is IDictionary;
        }

        internal static bool IsLinkedList(object obj)
        {
            Type[] genericTypes = obj.GetType().GenericTypeArguments;

            if (genericTypes.Length == 1)
            {
                var listType = typeof(LinkedList<>);
                var constructedListType = listType.MakeGenericType(genericTypes);
                return HasConversionOperator(obj.GetType(), constructedListType);
            }

            return false;
        }

        internal static bool HasConversionOperator(Type from, Type to)
        {
            UnaryExpression BodyFunction(Expression body) => Expression.Convert(body, to);
            ParameterExpression inp = Expression.Parameter(from, "inp");
            try
            {
                Expression.Lambda(BodyFunction(inp), inp).Compile();
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        internal static IEnumerable<FieldInfo> GetFieldsAccessibleForSerializer(this Type type)
        {
            return type.GetTypeInfo().DeclaredFields.Where(x => !x.IsStatic && !x.IsInitOnly);
        }

        internal static bool IsBaseTypeSupportedBySerializer(this Type type)
        {
            var serializedType = type.GetSerializedType();
            return
                serializedType != SerializedType.Unknown &&
                serializedType != SerializedType.IEnumerable &&
                serializedType != SerializedType.KeyValuePair;
        }
    }
}

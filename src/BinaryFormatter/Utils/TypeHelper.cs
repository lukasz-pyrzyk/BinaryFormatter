using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public static bool IsDictionary(object obj)
        {
            return obj is IDictionary;
        }

        public static bool IsList(object obj)
        {
            return obj is IList;
        }

        public static bool IsLinkedList(object obj)
        {
            Type[] genericTypes = obj.GetType().GenericTypeArguments;

            if (genericTypes.Length == 1)
            {
                var listType = typeof(LinkedList<>);
                var constructedListType = listType.MakeGenericType(genericTypes);
                return HasConversionOperator(obj.GetType(), constructedListType);
            } else
            {
                return false;
            }          
        }

        public static bool HasConversionOperator(Type from, Type to)
        {
            Func<Expression, UnaryExpression> bodyFunction = body => Expression.Convert(body, to);
            ParameterExpression inp = Expression.Parameter(from, "inp");
            try
            {                
                Expression.Lambda(bodyFunction(inp), inp).Compile();
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}

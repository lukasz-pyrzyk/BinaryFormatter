using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Numerics;

namespace BinaryFormatter.Utils
{
    public static class TypeInfoExtensions
    {
        private static readonly List<TypeInfo> _baseTypes = new List<TypeInfo>
        {
            typeof(byte).GetTypeInfo(),
            typeof(sbyte).GetTypeInfo(),
            typeof(char).GetTypeInfo(),
            typeof(short).GetTypeInfo(),
            typeof(ushort).GetTypeInfo(),
            typeof(int).GetTypeInfo(),
            typeof(uint).GetTypeInfo(),
            typeof(long).GetTypeInfo(),
            typeof(ulong).GetTypeInfo(),
            typeof(float).GetTypeInfo(),
            typeof(double).GetTypeInfo(),
            typeof(bool).GetTypeInfo(),
            typeof(decimal).GetTypeInfo(),
            typeof(string).GetTypeInfo(),
            typeof(DateTime).GetTypeInfo(),
            typeof(byte[]).GetTypeInfo(),
            typeof(Guid).GetTypeInfo(),
            typeof(Uri).GetTypeInfo(),
            typeof(KeyValuePair<,>).GetTypeInfo(),
            typeof(BigInteger).GetTypeInfo()
        };

        public static IEnumerable<ConstructorInfo> GetAllConstructors(this TypeInfo typeInfo)
            => GetAll(typeInfo, ti => ti.DeclaredConstructors);

        public static IEnumerable<EventInfo> GetAllEvents(this TypeInfo typeInfo)
            => GetAll(typeInfo, ti => ti.DeclaredEvents);

        public static IEnumerable<FieldInfo> GetAllFields(this TypeInfo typeInfo)
            => GetAll(typeInfo, ti => ti.DeclaredFields);

        public static IEnumerable<MemberInfo> GetAllMembers(this TypeInfo typeInfo)
            => GetAll(typeInfo, ti => ti.DeclaredMembers);

        public static IEnumerable<MethodInfo> GetAllMethods(this TypeInfo typeInfo)
            => GetAll(typeInfo, ti => ti.DeclaredMethods);

        public static IEnumerable<TypeInfo> GetAllNestedTypes(this TypeInfo typeInfo)
            => GetAll(typeInfo, ti => ti.DeclaredNestedTypes);

        public static IEnumerable<PropertyInfo> GetAllProperties(this TypeInfo typeInfo)
            => GetAll(typeInfo, ti => ti.DeclaredProperties);

        private static IEnumerable<T> GetAll<T>(TypeInfo typeInfo, Func<TypeInfo, IEnumerable<T>> accessor)
        {
            while (typeInfo != null)
            {
                foreach (var t in accessor(typeInfo))
                {
                    yield return t;
                }

                typeInfo = typeInfo.BaseType?.GetTypeInfo();
            }
        }

        public static bool IsBaseType(this TypeInfo typeInfo)
        {
            return _baseTypes.Any(bt => bt == typeInfo);
        }
    }
}

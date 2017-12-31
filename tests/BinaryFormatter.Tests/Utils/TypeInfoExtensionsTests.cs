using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using BinaryFormatter.Utils;
using Xunit;

namespace BinaryFormatter.Tests.Utils
{
    public class TypeInfoExtensionsTests
    {
        class Master
        {
            public string Name { get; set; }
            public event EventHandler TestEvent1;

            class Nested1
            {
                Nested1()
                {
                }
            }
        }
        class Slave : Master
        {
            public int Priority { get; set; }
            public event EventHandler TestEvent2;

            class Nested2
            {
                Nested2()
                {
                }
            }
        }

        [Fact]
        public void GetAllConstructors()
        {
            Slave testObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<ConstructorInfo> allConstructors = testObject.GetType().GetTypeInfo().GetAllConstructors();

            Assert.Equal(allConstructors.Count(), 3);
        }

        [Fact]
        public void GetAllEvents()
        {
            Slave testObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<EventInfo> allEvents = testObject.GetType().GetTypeInfo().GetAllEvents();

            Assert.Equal(allEvents.Count(), 2);
        }

        [Fact]
        public void GetAllFields()
        {
            Slave testObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<FieldInfo> allFields = testObject.GetType().GetTypeInfo().GetAllFields();

            Assert.Equal(allFields.Count(), 4);
        }

        [Fact]
        public void GetAllMembers()
        {
            Slave testObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<MemberInfo> allMembers = testObject.GetType().GetTypeInfo().GetAllMembers();

            Assert.Equal(allMembers.Count(), 32);
        }

        [Fact]
        public void GetAllMethods()
        {
            Slave testObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<MemberInfo> allMethods = testObject.GetType().GetTypeInfo().GetAllMethods();

            Assert.Equal(allMethods.Count(), 19);
        }

        [Fact]
        public void GetAllNestedTypes()
        {
            Slave testObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<MemberInfo> allNestedTypes = testObject.GetType().GetTypeInfo().GetAllNestedTypes();

            Assert.Equal(allNestedTypes.Count(), 2);
        }

        [Fact]
        public void GetAllProperties()
        {
            Slave testObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<PropertyInfo> allProperties = testObject.GetType().GetTypeInfo().GetAllProperties();

            Assert.Equal(allProperties.Count(), 2);
        }

        [Fact]
        public void IsBaseType()
        {
            Assert.True((typeof(byte)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(sbyte)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(char)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(short)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(ushort)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(int)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(uint)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(long)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(ulong)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(float)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(double)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(decimal)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(string)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(DateTime)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(TimeSpan)).GetTypeInfo().IsBaseType());
            Assert.True((typeof(byte[])).GetTypeInfo().IsBaseType());
            Assert.True((typeof(BigInteger)).GetTypeInfo().IsBaseType());
        }
    }
}

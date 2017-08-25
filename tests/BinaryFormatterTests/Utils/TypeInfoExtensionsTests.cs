using System.Reflection;
using BinaryFormatter.Utils;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Numerics;

namespace BinaryFormatterTests.Utils
{
    public class TypeInfoExtensionsTests
    {
        class Master
        {
            public string Name { get; set; }
            public event EventHandler testEvent_1;

            class Nested_1
            {
                Nested_1()
                {
                }
            }
        }
        class Slave : Master
        {
            public int Priority { get; set; }
            public event EventHandler testEvent_2;

            class Nested_2
            {
                Nested_2()
                {
                }
            }
        }

        [Fact]
        public void GetAllConstructors()
        {
            Slave TestObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<ConstructorInfo> allConstructors = TestObject.GetType().GetTypeInfo().GetAllConstructors();

            Assert.Equal(allConstructors.Count(), 3);
        }

        [Fact]
        public void GetAllEvents()
        {
            Slave TestObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<EventInfo> allEvents = TestObject.GetType().GetTypeInfo().GetAllEvents();

            Assert.Equal(allEvents.Count(), 2);
        }

        [Fact]
        public void GetAllFields()
        {
            Slave TestObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<FieldInfo> allFields = TestObject.GetType().GetTypeInfo().GetAllFields();

            Assert.Equal(allFields.Count(), 4);
        }

        [Fact]
        public void GetAllMembers()
        {
            Slave TestObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<MemberInfo> allMembers = TestObject.GetType().GetTypeInfo().GetAllMembers();

            Assert.Equal(allMembers.Count(), 32);
        }

        [Fact]
        public void GetAllMethods()
        {
            Slave TestObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<MemberInfo> allMethods = TestObject.GetType().GetTypeInfo().GetAllMethods();

            Assert.Equal(allMethods.Count(), 19);
        }

        [Fact]
        public void GetAllNestedTypes()
        {
            Slave TestObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<MemberInfo> allNestedTypes = TestObject.GetType().GetTypeInfo().GetAllNestedTypes();

            Assert.Equal(allNestedTypes.Count(), 2);
        }

        [Fact]
        public void GetAllProperties()
        {
            Slave TestObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<PropertyInfo> allProperties = TestObject.GetType().GetTypeInfo().GetAllProperties();

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

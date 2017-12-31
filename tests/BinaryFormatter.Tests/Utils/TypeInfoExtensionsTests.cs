using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using BinaryFormatter.Utils;
using FluentAssertions;
using Xunit;
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable EventNeverSubscribedTo.Local
// ReSharper disable All
#pragma warning disable 67

namespace BinaryFormatter.Tests.Utils
{
    public class TypeInfoExtensionsTests
    {
        private readonly Slave testObject = new Slave { Name = "Test 1", Priority = 123 };

        [Fact]
        public void GetAllConstructors()
        {
            IEnumerable<ConstructorInfo> allConstructors = testObject.GetType().GetTypeInfo().GetAllConstructors();

            allConstructors.Should().HaveCount(3);
        }

        [Fact]
        public void GetAllEvents()
        {
            IEnumerable<EventInfo> allEvents = testObject.GetType().GetTypeInfo().GetAllEvents();

            allEvents.Should().HaveCount(2);
        }

        [Fact]
        public void GetAllFields()
        {
            IEnumerable<FieldInfo> allFields = testObject.GetType().GetTypeInfo().GetAllFields();

            allFields.Should().HaveCount(4);
        }

        [Fact]
        public void GetAllMembers()
        {
            IEnumerable<MemberInfo> allMembers = testObject.GetType().GetTypeInfo().GetAllMembers();

            allMembers.Should().HaveCount(32);
        }

        [Fact]
        public void GetAllMethods()
        {
            IEnumerable<MemberInfo> allMethods = testObject.GetType().GetTypeInfo().GetAllMethods();

            allMethods.Should().HaveCount(19);
        }

        [Fact]
        public void GetAllNestedTypes()
        {
            IEnumerable<MemberInfo> allNestedTypes = testObject.GetType().GetTypeInfo().GetAllNestedTypes();

            allNestedTypes.Should().HaveCount(2);
        }

        [Fact]
        public void GetAllProperties()
        {
            IEnumerable<PropertyInfo> allProperties = testObject.GetType().GetTypeInfo().GetAllProperties();

            allProperties.Should().HaveCount(2);
        }

        [Theory]
        [InlineData((typeof(byte)))]
        [InlineData((typeof(sbyte)))]
        [InlineData((typeof(char)))]
        [InlineData((typeof(short)))]
        [InlineData((typeof(ushort)))]
        [InlineData((typeof(int)))]
        [InlineData((typeof(uint)))]
        [InlineData((typeof(ulong)))]
        [InlineData((typeof(long)))]
        [InlineData((typeof(float)))]
        [InlineData((typeof(double)))]
        [InlineData((typeof(decimal)))]
        [InlineData((typeof(string)))]
        [InlineData((typeof(DateTime)))]
        [InlineData((typeof(TimeSpan)))]
        [InlineData((typeof(byte[])))]
        [InlineData((typeof(BigInteger)))]
        public void IsBaseType(Type type)
        {
            bool isBaseType = type.GetTypeInfo().IsBaseType();
            isBaseType.Should().BeTrue();
        }

        private class Master
        {
            public string Name { get; set; }
            public event EventHandler FirstEvent;

            private class Nested1
            {
            }
        }

        private class Slave : Master
        {
            public int Priority { get; set; }
            public event EventHandler SecondEvent;

            private class Nested2
            {
            }
        }
    }
}

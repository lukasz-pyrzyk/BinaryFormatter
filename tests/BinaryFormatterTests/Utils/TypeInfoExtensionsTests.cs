using System.Reflection;
using BinaryFormatter.Utils;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace BinaryFormatterTests.Utils
{
    public class TypeInfoExtensionsTests
    {
        class Master
        {
            public string Name { get; set; }
        }
        class Slave : Master
        {
            public int Priority { get; set; }
        }

        [Fact(Skip = "Work in progress")]
        public void GetAllConstructors()
        {

        }

        [Fact(Skip = "Work in progress")]
        public void GetAllEvents()
        {

        }

        [Fact(Skip = "Work in progress")]
        public void GetAllFields()
        {

        }

        [Fact(Skip = "Work in progress")]
        public void GetAllMembers()
        {

        }

        [Fact(Skip = "Work in progress")]
        public void GetAllMethods()
        {

        }

        [Fact(Skip = "Work in progress")]
        public void GetAllNestedTypes()
        {

        }

        [Fact]
        public void GetAllProperties()
        {
            Slave TestObject = new Slave() { Name = "Test 1", Priority = 123 };
            IEnumerable<PropertyInfo> allProperties = TestObject.GetType().GetTypeInfo().GetAllProperties();

            Assert.Equal(allProperties.Count(), 2);
        }
    }
}

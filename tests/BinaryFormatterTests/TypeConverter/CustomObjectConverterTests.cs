using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class CustomObjectConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize_SimpleObject()
        {
            var converter = new BinaryConverter();

            SimpleObject simpleObject = new SimpleObject() { Name = "John", Age = 50 };

            byte[] bytesSimpleObject = converter.Serialize(simpleObject);
            var valueFromBytesSimpleObject = converter.Deserialize<SimpleObject>(bytesSimpleObject);
            Assert.Equal(valueFromBytesSimpleObject.Name, simpleObject.Name);
            Assert.Equal(valueFromBytesSimpleObject.Age, simpleObject.Age);
        }

        [Fact (Skip = "Work in progress...")]
        public void CanSerializeAndDeserialize_ComplexObject()
        {
            var converter = new BinaryConverter();

            ComplexObject serializableRow = new ComplexObject();
            serializableRow.MasterRow = new ComplexObjectRow();
            //serializableRow.MasterRow.Data = new[] {
            //    new ComplexObjectColumn
            //    {
            //        Name = "Key",
            //        Value = "12345"
            //    },
            //    new ComplexObjectColumn
            //    {
            //        Name = "TestMasterName0",
            //        Value = "TestMasterValue0"
            //    }
            //};
            //serializableRow.DetailRows = new[] 
            //{
            //    new ComplexObjectRow
            //    {
            //        Data = new[] 
            //        {
            //            new ComplexObjectColumn { Name = "TestDetailName0", Value = "TestDetailValue0" },
            //            new ComplexObjectColumn { Name = "TestDetailName1", Value = "TestDetailValue1" }
            //        }
            //    }
            //};

            var rowBytes = converter.Serialize(serializableRow);
            var row = converter.Deserialize<ComplexObject>(rowBytes);
        }

        class SimpleObject
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class ComplexObject
        {
            public ComplexObjectRow MasterRow { get; set; }
            public IEnumerable<ComplexObjectRow> DetailRows { get; set; }
        }

        public class ComplexObjectColumn
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public class ComplexObjectRow
        {
            public IEnumerable<ComplexObjectColumn> Data { get; set; }
        }
    }
}

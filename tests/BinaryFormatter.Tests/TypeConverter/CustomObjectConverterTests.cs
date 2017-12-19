using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace BinaryFormatter.Tests.TypeConverter
{
    public class CustomObjectConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserialize_SimpleObject()
        {
            // Arrange
            var simpleObject = new SimpleObject { Name = "John", Age = 50 };

            // Act
            var deserialized = TestHelper.SerializeAndDeserialize(simpleObject);

            // Assert
            deserialized.Name.Should().Be(simpleObject.Name);
            deserialized.Age.Should().Be(simpleObject.Age);
        }

        [Fact]
        public void CanSerializeAndDeserialize_ComplexObject()
        {
            // Arrange
            var obj = new ComplexObject();
            obj.MasterRow = new ComplexObjectRow();
            obj.MasterRow.Data = new[]
            {
                new ComplexObjectColumn { Name = "FirstKey", Value = "12345" },
                new ComplexObjectColumn { Name = "SecondKey", Value = "98776" }
            };
            obj.DetailRows = new[]
            {
                new ComplexObjectRow
                {
                    Data = new []
                    {
                        new ComplexObjectColumn { Name = "FirstColumn", Value = "FirstValue" },
                        new ComplexObjectColumn { Name = "SecondColumn", Value = "SecondValue" }
                    }
                }
            };

            // Act
            var deserialized = TestHelper.SerializeAndDeserialize(obj);

            // Assert
            Compare(obj.MasterRow.Data, deserialized.MasterRow.Data);
            Compare(obj.DetailRows, deserialized.DetailRows);
        }

        private void Compare<TBefore, TAfter>(IEnumerable<TBefore> columnsBefore, IEnumerable<TAfter> columnsAfter)
        {
            var before = columnsBefore.ToArray();
            var after = columnsAfter?.ToArray();

            after.Should().BeEquivalentTo(before);
        }

        private class SimpleObject
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        private class ComplexObject
        {
            public ComplexObjectRow MasterRow { get; set; }
            public IEnumerable<ComplexObjectRow> DetailRows { get; set; }
        }

        private class ComplexObjectColumn
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        private class ComplexObjectRow
        {
            public IEnumerable<ComplexObjectColumn> Data { get; set; }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
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
            deserialized.Age.Should().Be(simpleObject.Age);
        }

        [Fact]
        public void CanSerializeAndDeserialize_ComplexObject()
        {
            ComplexObject serializableRow = new ComplexObject();
            serializableRow.MasterRow = new ComplexObjectRow();
            serializableRow.MasterRow.Data = new[] { new ComplexObjectColumn { Name = "Key", Value = "12345" } };
            serializableRow.DetailRows = new[]
            {
                new ComplexObjectRow
                {
                    Data = new[] 
                    {
                        new ComplexObjectColumn { Name = "TestDetailName0", Value = "TestDetailValue0" },
                    }
                }
            };

            var deserialized = TestHelper.SerializeAndDeserialize(serializableRow);

            //TODO Find solution for easiest way to compare of two object

            var serializableRow_MasterRow_Data = (IList)serializableRow.MasterRow.Data;
            var deserializedRow_MasterRow_Data = (IList)deserialized.MasterRow.Data;
            Assert.Equal(serializableRow_MasterRow_Data.Count, ((IList)deserialized.MasterRow.Data).Count);
            Assert.Equal((serializableRow_MasterRow_Data[0] as ComplexObjectColumn).Name, (deserializedRow_MasterRow_Data[0] as ComplexObjectColumn).Name);
            Assert.Equal((serializableRow_MasterRow_Data[0] as ComplexObjectColumn).Value, (deserializedRow_MasterRow_Data[0] as ComplexObjectColumn).Value);
            Assert.Equal((serializableRow_MasterRow_Data[1] as ComplexObjectColumn).Name, (deserializedRow_MasterRow_Data[1] as ComplexObjectColumn).Name);
            Assert.Equal((serializableRow_MasterRow_Data[1] as ComplexObjectColumn).Value, (deserializedRow_MasterRow_Data[1] as ComplexObjectColumn).Value);

            var serializableRow_DetailRows = (IList)serializableRow.DetailRows;
            var derializableRow_DetailRows = (IList)deserialized.DetailRows;
            Assert.Equal(serializableRow_DetailRows.Count, derializableRow_DetailRows.Count);
            Assert.Equal(((IList)(serializableRow_DetailRows[0] as ComplexObjectRow).Data).Count, ((IList)(((IList)deserialized.DetailRows)[0] as ComplexObjectRow).Data).Count);
            Assert.Equal((((IList)(serializableRow_DetailRows[0] as ComplexObjectRow).Data)[0] as ComplexObjectColumn).Name, (((IList)(derializableRow_DetailRows[0] as ComplexObjectRow).Data)[0] as ComplexObjectColumn).Name);
            Assert.Equal((((IList)(serializableRow_DetailRows[0] as ComplexObjectRow).Data)[0] as ComplexObjectColumn).Value, (((IList)(derializableRow_DetailRows[0] as ComplexObjectRow).Data)[0] as ComplexObjectColumn).Value);
            Assert.Equal((((IList)(serializableRow_DetailRows[0] as ComplexObjectRow).Data)[1] as ComplexObjectColumn).Name, (((IList)(derializableRow_DetailRows[0] as ComplexObjectRow).Data)[1] as ComplexObjectColumn).Name);
            Assert.Equal((((IList)(serializableRow_DetailRows[0] as ComplexObjectRow).Data)[1] as ComplexObjectColumn).Value, (((IList)(derializableRow_DetailRows[0] as ComplexObjectRow).Data)[1] as ComplexObjectColumn).Value);
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

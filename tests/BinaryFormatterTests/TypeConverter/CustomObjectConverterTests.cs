using System.Collections;
using System.Collections.Generic;
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

            SimpleObject simpleObject = new SimpleObject { Name = "John", Age = 50 };

            byte[] bytesSimpleObject = converter.Serialize(simpleObject);
            var valueFromBytesSimpleObject = converter.Deserialize<SimpleObject>(bytesSimpleObject);
            Assert.Equal(valueFromBytesSimpleObject.Name, simpleObject.Name);
            Assert.Equal(valueFromBytesSimpleObject.Age, simpleObject.Age);
        }

        [Fact]
        public void CanSerializeAndDeserialize_ComplexObject()
        {
            var converter = new BinaryConverter();

            ComplexObject serializableRow = new ComplexObject();
            serializableRow.MasterRow = new ComplexObjectRow();
            serializableRow.MasterRow.Data = new[] {
                new ComplexObjectColumn
                {
                    Name = "Key",
                    Value = "12345"
                },
                new ComplexObjectColumn
                {
                    Name = "TestMasterName0",
                    Value = "TestMasterValue0"
                }
            };
            serializableRow.MasterRow.Data = new ComplexObjectColumn[0];
            serializableRow.DetailRows = new[]
            {
                new ComplexObjectRow
                {
                    Data = new[]
                    {
                        new ComplexObjectColumn { Name = "TestDetailName0", Value = "TestDetailValue0" },
                        new ComplexObjectColumn { Name = "TestDetailName1", Value = "TestDetailValue1" }
                    }
                }
            };

            var rowBytes = converter.Serialize(serializableRow);
            var deserializedRow = converter.Deserialize<ComplexObject>(rowBytes);

            //TODO Find solution for easiest way to compare of two object

            var serializableRow_MasterRow_Data = (IList)serializableRow.MasterRow.Data;
            var deserializedRow_MasterRow_Data = (IList)deserializedRow.MasterRow.Data;
            Assert.Equal(serializableRow_MasterRow_Data.Count, ((IList)deserializedRow.MasterRow.Data).Count);
            Assert.Equal((serializableRow_MasterRow_Data[0] as ComplexObjectColumn).Name, (deserializedRow_MasterRow_Data[0] as ComplexObjectColumn).Name);
            Assert.Equal((serializableRow_MasterRow_Data[0] as ComplexObjectColumn).Value, (deserializedRow_MasterRow_Data[0] as ComplexObjectColumn).Value);
            Assert.Equal((serializableRow_MasterRow_Data[1] as ComplexObjectColumn).Name, (deserializedRow_MasterRow_Data[1] as ComplexObjectColumn).Name);
            Assert.Equal((serializableRow_MasterRow_Data[1] as ComplexObjectColumn).Value, (deserializedRow_MasterRow_Data[1] as ComplexObjectColumn).Value);

            var serializableRow_DetailRows = (IList)serializableRow.DetailRows;
            var derializableRow_DetailRows = (IList)deserializedRow.DetailRows;
            Assert.Equal(serializableRow_DetailRows.Count, derializableRow_DetailRows.Count);
            Assert.Equal(((IList)(serializableRow_DetailRows[0] as ComplexObjectRow).Data).Count, ((IList)(((IList)deserializedRow.DetailRows)[0] as ComplexObjectRow).Data).Count);
            Assert.Equal((((IList)(serializableRow_DetailRows[0] as ComplexObjectRow).Data)[0] as ComplexObjectColumn).Name, (((IList)(derializableRow_DetailRows[0] as ComplexObjectRow).Data)[0] as ComplexObjectColumn).Name);
            Assert.Equal((((IList)(serializableRow_DetailRows[0] as ComplexObjectRow).Data)[0] as ComplexObjectColumn).Value, (((IList)(derializableRow_DetailRows[0] as ComplexObjectRow).Data)[0] as ComplexObjectColumn).Value);
            Assert.Equal((((IList)(serializableRow_DetailRows[0] as ComplexObjectRow).Data)[1] as ComplexObjectColumn).Name, (((IList)(derializableRow_DetailRows[0] as ComplexObjectRow).Data)[1] as ComplexObjectColumn).Name);
            Assert.Equal((((IList)(serializableRow_DetailRows[0] as ComplexObjectRow).Data)[1] as ComplexObjectColumn).Value, (((IList)(derializableRow_DetailRows[0] as ComplexObjectRow).Data)[1] as ComplexObjectColumn).Value);
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

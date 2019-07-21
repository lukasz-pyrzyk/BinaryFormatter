using System.IO;
using FluentAssertions;
using Xunit;

namespace BinaryFormatter.Tests
{
    public class WhenSerializingFields
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            // arrange
            var obj = TestHelper.Create<SimpleClassWithFields>();

            // act
            var fromBytes = TestHelper.SerializeAndDeserialize(obj);

            // assert
            fromBytes.Should().NotBeNull();
            fromBytes.StreamName.Should().Be(obj.StreamName);
            fromBytes.StreamType.Should().Be(obj.StreamType);
            fromBytes.StreamSize.Should().Be(obj.StreamSize);
            fromBytes.StreamContent.Should().Be(obj.StreamContent);
        }

        [Fact]
        public void CanSerializeAndDeserializeWithStaticField()
        {
            // arrange
            var staticValueBefore = SimpleClassWithFieldsAndStaticField.StaticField;
            var obj = TestHelper.Create<SimpleClassWithFieldsAndStaticField>();

            // act
            var converter = new BinaryConverter();
            var stream = new MemoryStream();
            converter.Serialize(obj, stream);
            stream.Seek(0, SeekOrigin.Begin);
            var fromBytes = converter.Deserialize<SimpleClassWithFieldsAndStaticField>(stream.ToArray());

            // assert
            fromBytes.Should().NotBeNull();
            fromBytes.NormalField.Should().Be(obj.NormalField);
            SimpleClassWithFieldsAndStaticField.StaticField.Should().Be(staticValueBefore);
            stream.Length.Should().BeLessThan(300, "long static value shouldn't be added to the stream");
        }

        [Fact]
        public void CanSerializeAndDeserializeWithReadonlyField()
        {
            // arrange
            var obj = TestHelper.Create<SimpleClassWithFieldsAndReadonlyField>();

            // act
            var converter = new BinaryConverter();
            var stream = new MemoryStream();
            converter.Serialize(obj, stream);
            stream.Seek(0, SeekOrigin.Begin);
            var fromBytes = converter.Deserialize<SimpleClassWithFieldsAndReadonlyField>(stream.ToArray());

            // assert
            fromBytes.Should().NotBeNull();
            fromBytes.NormalField.Should().Be(obj.NormalField);
            fromBytes.ReadonlyField.Should().Be(obj.ReadonlyField);
            stream.Length.Should().BeLessThan(300, "long readonly value shouldn't be added to the stream");
        }

        public class SimpleClassWithFields
        {
            public string StreamName;
            public int StreamType;
            public float StreamSize;
            public byte StreamContent;
        }

        public class SimpleClassWithFieldsAndStaticField
        {
            public static string StaticField = new string('t', 99999);
            public string NormalField;
        }

        public class SimpleClassWithFieldsAndReadonlyField
        {
            public readonly string ReadonlyField = new string('t', 99999);
            public string NormalField;
        }
    }
}

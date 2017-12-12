using BinaryFormatter;
using FluentAssertions;
using Xunit;

namespace BinaryFormatterTests
{
    public class WhenSerializingPrimitives
    {
        [Fact]
        public void Primitive_CanBeSerializedAndDeserialized()
        {
            // Arrange
            var age = 23;

            var converter = new BinaryConverter();
            byte[] serialized = converter.Serialize(age);

            // act
            var deserialized = converter.Deserialize<int>(serialized);

            // assert
            deserialized.Should().Be(age);
        }

        [Fact]
        public void PrimitiveInClasses_CanBeSerializedAndDeserialized()
        {
            // Arrange
            var age = 23;
            var obj = new ClassWithEnum { Age = age };

            var converter = new BinaryConverter();
            byte[] serialized = converter.Serialize(obj);

            // act
            var deserialized = converter.Deserialize<ClassWithEnum>(serialized);

            // assert
            deserialized.Should().NotBeNull();
            deserialized.Age.Should().Be(age);
        }

        [Fact]
        public void PrimitiveInClasses_WithDifferentProperties_CanBeSerializedAndDeserialized()
        {
            // Arrange
            var age = 23;
            var isAdult = true;
            var obj = new ClassWithIntAndBool { Age = age, IsAdult = isAdult };

            var converter = new BinaryConverter();
            byte[] serialized = converter.Serialize(obj);

            // act
            var deserialized = converter.Deserialize<ClassWithIntAndBool>(serialized);

            // assert
            deserialized.Should().NotBeNull();
            deserialized.Age.Should().Be(age);
            deserialized.IsAdult.Should().Be(isAdult);
        }
        

        private class ClassWithEnum
        {
            public int Age { get; set; }
        }

        private class ClassWithIntAndBool
        {
            public int Age { get; set; }
            public bool IsAdult { get; set; }
        }
    }
}

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

            // act
            var deserialized = TestHelper.SerializeAndDeserialize(age);

            // assert
            deserialized.Should().Be(age);
        }

        [Fact]
        public void PrimitiveInClasses_CanBeSerializedAndDeserialized()
        {
            // Arrange
            var age = 23;
            var obj = new ClassWithEnum { Age = age };
            
            // act
            var deserialized = TestHelper.SerializeAndDeserialize(obj);

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

            // act
            var deserialized = TestHelper.SerializeAndDeserialize(obj);

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

using FluentAssertions;
using Xunit;

namespace BinaryFormatter.Tests
{
    public class WhenSerializingEnum
    {
        [Fact]
        public void Enum_CanBeSerializedAndDeserialized()
        {
            // Arrange
            var gender = Gender.Male;

            // act
            var deserialized = TestHelper.SerializeAndDeserialize(gender);

            // assert
            deserialized.Should().Be(gender);
        }

        [Fact]
        public void EnumInClasses_CanBeSerializedAndDeserialized()
        {
            // Arrange
            var gender = Gender.Male;
            var obj = new ClassWithEnum { Gender = gender };

            // act
            var deserialized = TestHelper.SerializeAndDeserialize(obj);

            // assert
            deserialized.Should().NotBeNull();
            deserialized.Gender.Should().Be(gender);
        }

        [Fact]
        public void EnumInClasses_WithDifferentProperties_CanBeSerializedAndDeserialized()
        {
            // Arrange
            var gender = Gender.Male;
            var isAdult = true;
            var obj = new ClassWithEnumAndBool { Gender = gender, IsAdult = isAdult };

            // act
            var deserialized = TestHelper.SerializeAndDeserialize(obj);

            // assert
            deserialized.Should().NotBeNull();
            deserialized.Gender.Should().Be(gender);
            deserialized.IsAdult.Should().Be(isAdult);
        }

        private enum Gender
        {
            Unknown = 0,
            Male = 1,
            Female = 2
        }

        private class ClassWithEnum
        {
            public Gender Gender { get; set; }
        }

        private class ClassWithEnumAndBool
        {
            public Gender Gender { get; set; }
            public bool IsAdult { get; set; }
        }
    }
}

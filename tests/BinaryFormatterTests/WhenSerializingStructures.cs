using FluentAssertions;
using Xunit;

namespace BinaryFormatterTests
{
    public class WhenSerializingStructures
    {
        [Fact]
        public void Structure_CanBeSerializedAndDeserialized()
        {
            // Arrange
            var person = new Person { Id = 5 };

            // act
            var deserialized = TestHelper.SerializeAndDeserialize(person);

            // assert
            deserialized.Should().Be(person);
        }

        [Fact]
        public void StructureInClasses_CanBeSerializedAndDeserialized()
        {
            // Arrange
            var person = new Person { Id = 8 };
            var obj = new ClassWithStruct { Person = person };

            // act
            var deserialized = TestHelper.SerializeAndDeserialize(obj);

            // assert
            deserialized.Should().NotBeNull();
            deserialized.Person.Should().Be(person);
        }

        [Fact]
        public void StructureInClasses_WithDifferentProperties_CanBeSerializedAndDeserialized()
        {
            // Arrange
            var person = new Person { Id = 7 };
            var isAdult = true;
            var obj = new ClassWithStructAndBool { Person = person, IsAdult = isAdult };
            
            // act
            var deserialized = TestHelper.SerializeAndDeserialize(obj);

            // assert
            deserialized.Should().NotBeNull();
            deserialized.Person.Should().Be(person);
            deserialized.IsAdult.Should().Be(isAdult);
        }

        private struct Person
        {
            public int Id { get; set; }
        }

        private class ClassWithStruct
        {
            public Person Person { get; set; }
        }

        private class ClassWithStructAndBool
        {
            public Person Person { get; set; }
            public bool IsAdult { get; set; }
        }
    }
}

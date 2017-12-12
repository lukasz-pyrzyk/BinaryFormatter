using BinaryFormatter;
using FluentAssertions;
using Xunit;

namespace BinaryFormatterTests
{
    public class WhenSerializingEnum
    {
        [Fact]
        public void EnumCanBeSerializedAndDeserialized()
        {
            // Arrange
            var gender = Gender.Male;

            var converter = new BinaryConverter();
            byte[] serialized = converter.Serialize(gender);

            // act
            var deserialized = converter.Deserialize<Gender>(serialized);

            // assert
            deserialized.Should().Be(gender);
        }

        private enum Gender
        {
            Unknown = 0,
            Male = 1,
            Female = 2
        }
    }
}

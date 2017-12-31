using System;
using FluentAssertions;
using Xunit;
// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedMember.Local

namespace BinaryFormatter.Tests
{
    public class WhenSerializingClasses
    {
        [Fact]
        public void WithoutCustomCtor_WithProperties_WithPublicSetter()
        {
            var before = new ObjWithoutCtor { Int = 1, Double = 1, String = "lorem ipsum" };

            var after = TestHelper.SerializeAndDeserialize(before);

            after.Should().BeEquivalentTo(before);
        }

        [Fact]
        public void ReadonlyPropertiesAreSkipped()
        {
            var before = new ObjWithReadonlyProperties
            {
                Name = "John",
                BirthDay = DateTime.Now.AddYears(-50)
            };

            var after = TestHelper.SerializeAndDeserialize(before);

            after.Should().BeEquivalentTo(before);
        }

        private class ObjWithoutCtor
        {
            public int Int { get; set; }
            public double Double { get; set; }
            public string String { get; set; }
        }

        private class ObjWithReadonlyProperties
        {
            public string Name { get; set; }
            public DateTime BirthDay { get; set; }
            public int Age => DateTime.Now.Year - BirthDay.Year;
        }
    }
}

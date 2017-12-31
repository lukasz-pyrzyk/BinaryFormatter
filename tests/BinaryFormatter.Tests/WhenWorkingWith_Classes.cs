using System;
using Xunit;

namespace BinaryFormatter.Tests
{
    public class WhenWorkingWith_Classes
    {
        class WithoutCtor
        {
            public int Int { get; set; }
            public double Double { get; set; }
            public string String { get; set; }
        }

        class WithReadonlyProperties
        {
            public string Name { get; set; }
            public DateTime BirthDay { get; set; }
            public int Age => DateTime.Now.Year - BirthDay.Year;
        }

        [Fact]
        public void CanWorkWith_ClassesWithoutCustomCtor_WithProperties_WithPublicSetter()
        {
            var before = new WithoutCtor { Int = 1, Double = 1, String = "lorem ipsum" };

            var formatter = new BinaryConverter();
            byte[] data = formatter.Serialize(before);

            var after = formatter.Deserialize<WithoutCtor>(data);

            Assert.Equal(before.String, after.String);
            Assert.Equal(before.Int, after.Int);
            Assert.Equal(before.Double, after.Double);
        }

        [Fact]
        public void CanWorkWith_Classes_WithReadonlyProperties()
        {
            var before = new WithReadonlyProperties
            {
                Name = "John",
                BirthDay = DateTime.Now.AddYears(-50)
            };

            var formatter = new BinaryConverter();
            byte[] data = formatter.Serialize(before);
            var after = formatter.Deserialize<WithReadonlyProperties>(data);

            Assert.Equal(before.Name, after.Name);
            Assert.Equal(before.BirthDay, after.BirthDay);
            Assert.Equal(before.Age, after.Age);
        }
    }
}

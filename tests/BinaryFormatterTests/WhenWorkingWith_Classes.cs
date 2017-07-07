using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests
{
    public class WhenWorkingWith_Classes
    {
        class WithoutCtor
        {
            public int Int { get; set; }
            public double Double { get; set; }
            public string String { get; set; }
        }

        class WithCtor
        {
            public WithCtor(int i, double d, string s)
            {
                Int = i;
                Double = d;
                String = s;
            }

            public int Int { get; set; }
            public double Double { get; set; }
            public string String { get; set; }
        }

        [Fact]
        public void CanWorkWith_ClassesWithoutCustomCtor_WithProperties_WithPublicSetter()
        {
            var before = new WithoutCtor(){Int = 1, Double = 1, String = "lorem ipsum"};

            var formatter = new BinaryConverter();
            byte[] data = formatter.Serialize(before);

            var after = formatter.Deserialize<WithoutCtor>(data);

            Assert.Equal(before.String, after.String);
            Assert.Equal(before.Int, after.Int);
            Assert.Equal(before.Double, after.Double);
        }

        [Fact(Skip = "Work in progress")]
        public void CanWorkWith_ClasseWithCustomCtor_WithProperties_WithPublicSetter()
        {
            var before = new WithCtor(1, 1, "lorem ipsum");

            var formatter = new BinaryConverter();
            byte[] data = formatter.Serialize(before);

            var after = formatter.Deserialize<WithCtor>(data);

            Assert.Equal(before.String, after.String);
            Assert.Equal(before.Int, after.Int);
            Assert.Equal(before.Double, after.Double);
        }
    }
}

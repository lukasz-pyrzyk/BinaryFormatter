using BinaryFormatter;
using Xunit;

namespace BinaryFormatterTests
{
    public class WhenWorkingWith_Structures
    {
        struct WithoutCtor
        {
            public int Int { get; set; }
            public double Double { get; set; }
            public string String { get; set; }
        }

        [Fact]
        public void CanWorkWith_StructuresWithoutCustomCtor_WithProperties_WithPublicSetter()
        {
            var before = new WithoutCtor() { Int = 1, Double = 1, String = "lorem ipsum" };

            var formatter = new BinaryConverter();
            byte[] data = formatter.Serialize(before);

            var after = formatter.Deserialize<WithoutCtor>(data);

            Assert.Equal(before.String, after.String);
            Assert.Equal(before.Int, after.Int);
            Assert.Equal(before.Double, after.Double);
        }
    }
}

using AutoFixture;
using BinaryFormatter;

namespace BinaryFormatterTests
{
    internal static class TestHelper
    {
        public static T SerializeAndDeserialize<T>(T obj)
        {
            var converter = new BinaryConverter();
            byte[] bytes = converter.Serialize(obj);

            var fromBytes = converter.Deserialize<T>(bytes);
            return fromBytes;
        }

        public static T Create<T>()
        {
            var fixture = new Fixture();

            return fixture.Create<T>();
        }
    }
}

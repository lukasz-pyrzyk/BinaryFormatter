using System;
using System.Text;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests.TypeConverter
{
    public class BaseTypeConverterTests
    {
        public const string Message = "Lorem ipsum";

        [Fact]
        public void CanSerializeWithSize()
        {
            Fake fake = new Fake();
            byte[] bytes = fake.Serialize(Message);

            int typeSize = BitConverter.ToInt32(bytes, 0);
            string word = Encoding.UTF8.GetString(bytes, sizeof (int), bytes.Length - sizeof (int));

            Assert.Equal(typeSize, Message.Length);
            Assert.Equal(word, Message);
        }

        [Fact]
        public void ThrowsWhenObjIsNull()
        {
            Fake fake = new Fake();

            Assert.ThrowsAny<ArgumentNullException>(() => fake.Serialize(null));
        }

        internal class Fake : BaseTypeConverter<string>
        {
            protected override int GetTypeSize()
            {
                return Message.Length;
            }

            protected override byte[] ProcessSerialize(string obj)
            {
                return Encoding.UTF8.GetBytes(obj);
            }
        }
    }
}
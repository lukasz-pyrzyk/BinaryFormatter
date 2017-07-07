using System;
using System.Collections.Generic;
using BinaryFormatter;
using BinaryFormatter.TypeConverter;
using Xunit;

namespace BinaryFormatterTests
{
    public class ConvertersSelectorTests
    {
        [Fact]
        public void ReturnsNull_WhenObjIsNull()
        {
            var selector = new ConvertersSelector();
            var converter = selector.SelectConverter((object)null);
            Assert.Null(converter);
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void CorrectlyMapsTypesToConverters(object obj, Type expectedType)
        {
            var selector = new ConvertersSelector();
            var converter = selector.SelectConverter(obj);
            Assert.Equal(converter.GetType(), expectedType);
        }

        public static IEnumerable<object[]> TestCases()
        {
            yield return new[] { (object)default(bool), typeof(BoolConverter) };
            yield return new[] { (object)new byte[0], typeof(ByteArrayConverter) };
            yield return new[] { (object)default(byte), typeof(ByteConverter) };
            yield return new[] { (object)default(char), typeof(CharConverter) };
            yield return new[] { (object)default(DateTime), typeof(DatetimeConverter) };
            yield return new[] { (object)default(decimal), typeof(DecimalConverter) };
            yield return new[] { (object)default(double), typeof(DoubleConverter) };
            yield return new[] { (object)default(float), typeof(FloatConverter) };
            yield return new[] { (object)new List<object>(), typeof(IEnumerableConverter) };
            yield return new[] { (object)default(long), typeof(LongConverter) };
            yield return new[] { (object)default(sbyte), typeof(SByteConverter) };
            yield return new[] { (object)default(short), typeof(ShortConverter) };
            yield return new[] { (object)string.Empty, typeof(StringConverter) };
            yield return new[] { (object)default(uint), typeof(UIntConverter) };
            yield return new[] { (object)default(ulong), typeof(ULongConverter) };
            yield return new[] { (object)default(ushort), typeof(UShortConverter) };
        }
    }
}

namespace BinaryFormatter.Types
{
    internal enum SerializedType : ushort
    {
        Unknown = 0,
        Null = 1,
        Byte = 2,
        Sbyte = 3,
        Char = 4,
        Short = 5,
        UShort = 6,
        Int = 7,
        Uint = 8,
        Long = 9,
        Ulong = 10,
        Float = 11,
        Double = 12,
        Bool = 13,
        Decimal = 14,
        String = 15,
        Datetime = 16,
        ByteArray = 17,
        IEnumerable = 18,
        Guid = 19,
        Uri = 20,
        Enum = 21,
        KeyValuePair = 22,        
        Timespan = 23,
        BitInteger = 24,
        CustomObject = 99
    }
}

namespace BinaryFormatter.Types
{
    internal enum SerializedType : ushort
    {        
        Null = 0,
        Byte = 1,
        Sbyte = 2,
        Char = 3,
        Short = 4,
        UShort = 5,
        Int = 6,
        Uint = 7,
        Long = 8,
        Ulong = 9,
        Float = 10,
        Double = 11,
        Bool = 12,
        Decimal = 13,
        String = 14,
        Datetime = 15,
        ByteArray = 16,
        IEnumerable = 17,
        Guid = 18,
        Uri = 19,
        Enum = 20,
        KeyValuePair = 21,        
        Timespan = 22,
        BitInteger = 23,
        CustomObject = 99
    }
}

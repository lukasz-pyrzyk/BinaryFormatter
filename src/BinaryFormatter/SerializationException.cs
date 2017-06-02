using System;

namespace BinaryFormatter
{
    class SerializationException : Exception
    {
        public SerializationException(string message) : base(message)
        {
        }
    }
}

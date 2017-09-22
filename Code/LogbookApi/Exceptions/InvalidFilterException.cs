using System;

namespace LogbookApi.Exceptions
{
    public class InvalidFilterException : Exception
    {
        public InvalidFilterException(string message) : base (message)
        { }
    }
}
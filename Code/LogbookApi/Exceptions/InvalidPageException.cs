using System;

namespace LogbookApi.Exceptions
{
    public class InvalidPageException : Exception
    {
        public InvalidPageException(string message) : base (message)
        { }
    }
}
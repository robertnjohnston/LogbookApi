using System;

namespace LogbookApi.Exceptions
{
    public class InvalidFlightException : Exception
    {
        public InvalidFlightException(string message) : base (message)
        { }
    }
}
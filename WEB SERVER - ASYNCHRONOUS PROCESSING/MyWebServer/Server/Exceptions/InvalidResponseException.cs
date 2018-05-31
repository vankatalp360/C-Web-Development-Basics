using System;

namespace MyWebServer.Server.Exceptions
{
    public class InvalidResponseException : Exception
    {
        public InvalidResponseException(string message)
            :base(message)
        {
            
        }
    }
}
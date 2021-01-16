using System;

namespace Exceptions
{
    public class WrongInputFormatException : ApplicationException
    {

        public WrongInputFormatException() : base("Error")
        {
        }

        public WrongInputFormatException(string g) : base("Error: " + g)
        {
        }

        public WrongInputFormatException(string g, Exception e)
        {
            throw new WrongInputFormatException("Error: " + g + " " + e.Message);
        }
    }

}
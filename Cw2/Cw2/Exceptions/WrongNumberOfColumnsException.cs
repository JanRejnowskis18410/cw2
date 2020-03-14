using System;
using System.Collections.Generic;
using System.Text;

namespace Cw2.Exceptions
{
    public class WrongNumberOfColumnsException : Exception
    {
        public WrongNumberOfColumnsException(string message) : base(message){}
    }
}

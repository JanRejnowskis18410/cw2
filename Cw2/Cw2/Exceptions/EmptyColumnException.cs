using System;
using System.Collections.Generic;
using System.Text;

namespace Cw2.Exceptions
{
    public class EmptyColumnException : Exception
    {
        public EmptyColumnException(string message) : base(message) { }
    }
}

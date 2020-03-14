using System;
using System.Collections.Generic;
using System.Text;

namespace Cw2.Exceptions
{
    class StudentAlreadyInFileException : Exception
    {
        public StudentAlreadyInFileException(string message) : base(message) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions
{
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException()
        {

        }
        public IncorrectPasswordException(string message) : base(message)
        {

        }
    }
}

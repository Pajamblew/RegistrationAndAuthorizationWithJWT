﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions
{
    public class IncorrectInputException : Exception
    {
        public IncorrectInputException()
        {

        }
        public IncorrectInputException(string? message) : base(message)
        {

        }
    }
}

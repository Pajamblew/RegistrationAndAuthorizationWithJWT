﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Errors
{
    public class UserExistsException : Exception
    {
        public UserExistsException() : base()
        {

        }
    }
}
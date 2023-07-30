using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IAuthManager
    {
        public string? Login(LoginBLModel loginModel);
        public string? Register(RegisterBLModel registerModel);
    }
}

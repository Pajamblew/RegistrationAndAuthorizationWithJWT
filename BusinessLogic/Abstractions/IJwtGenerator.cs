using BusinessLogic.Models;
using DBLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Abstractions
{
    public interface IJwtGenerator
    {
        public string GenerateJwt(User user);
    }
}

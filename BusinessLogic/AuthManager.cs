using BusinessLogic.Abstractions;
using BusinessLogic.Errors;
using BusinessLogic.Models;
using DBLibrary;
using DBLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic
{
    public class AuthManager : IAuthManager
    {
        private readonly DataContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtGenerator _jwtGenerator;
        public AuthManager(DataContext dataContext, IPasswordHasher passwordHasher, IJwtGenerator jwtGenerator)
        {
            _dbContext = dataContext;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
        }
        public string? Login(LoginBLModel loginModel)
        {
            if (loginModel.Password.IsNullOrEmpty() || loginModel.Username.IsNullOrEmpty())
            {
                throw new IncorrectInpuException();
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.Username == loginModel.Username);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            var result = _passwordHasher.Verify(user.PasswordHash, loginModel.Password);
            if (!result)
            {
                throw new IncorrectPasswordException();
            }
            var token = _jwtGenerator.GenerateJwt(user);
            return token;
        }
        public string? Register(RegisterBLModel registerModel)
        {
            if (registerModel.Name.IsNullOrEmpty() || registerModel.Password.IsNullOrEmpty() || registerModel.Username.IsNullOrEmpty())
            {
                throw new IncorrectInpuException();
            }
            if (_dbContext.Users.FirstOrDefault(u => u.Username == registerModel.Username) != null)
            {
                throw new UserExistsException();
            }

            var passwordHash = _passwordHasher.Hash(registerModel.Password);
            var user = new User()
            {
                Username = registerModel.Username,
                PasswordHash = passwordHash,
                Name = registerModel.Name,
                Surname = registerModel.Surname,
                Email = registerModel.Email,
                Phone = registerModel.Phone,
            };
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            var token = _jwtGenerator.GenerateJwt(user);

            return token;
        }
        
    }
}
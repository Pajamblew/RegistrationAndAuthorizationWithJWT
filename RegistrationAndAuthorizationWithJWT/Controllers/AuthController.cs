using BusinessLogic;
using BusinessLogic.Errors;
using DBLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistrationAndAuthorizationWithJWT.ViewModels;

namespace RegistrationAndAuthorizationWithJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route(nameof(Login))]
        public IActionResult Login([FromBody] LoginViewModel loginModel)
        {
            try
            {
                var token = _authManager.Login(loginModel.LoginToBLModel());
                if (token != null)
                    return Ok(token);
                else
                    return NotFound("User not found");
            }
            catch (IncorrectInpuException)
            {
                return NotFound("Incorrect data");
            }
            catch (UserNotFoundException)
            {
                return NotFound("User not found");
            }
            catch (IncorrectPasswordException)
            {
                return NotFound("Incorrect password");
            }
        }
        [HttpPost]
        [Route(nameof(Register))]
        public IActionResult Register([FromBody] RegisterViewModel registerModel)
        {
            try
            {
                var token = _authManager.Register(registerModel.RegisterToBLModel());
                if (token != null)
                    return Ok(token);
                else
                    return NotFound("Incorrect data");
            }
            catch (IncorrectInpuException)
            {
                return NotFound("Incorrect data");
            }
            catch (UserExistsException)
            {
                return NotFound("User with this Username already exists");
            }
        }

    }
}

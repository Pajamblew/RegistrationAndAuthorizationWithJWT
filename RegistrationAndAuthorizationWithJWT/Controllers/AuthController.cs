using BusinessLogic;
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
        [HttpPost]
        [Route(nameof(Login))]
        public IActionResult Login([FromBody] LoginViewModel loginModel)
        {
            try
            {
                var token = _authManager.Login(loginModel.LoginToBLModel());

                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(nameof(Register))]
        public IActionResult Register([FromBody] RegisterViewModel registerModel)
        {
            try
            {
                var token = _authManager.Register(registerModel.RegisterToBLModel());

                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

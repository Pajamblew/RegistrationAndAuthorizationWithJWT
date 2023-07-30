using BusinessLogic.Models;

namespace RegistrationAndAuthorizationWithJWT.ViewModels
{
    public static class ExtensionViewModel
    {
        public static LoginBLModel LoginToBLModel(this LoginViewModel loginViewModel)
        {
            return new LoginBLModel 
            { 
                Username = loginViewModel.Username, 
                Password = loginViewModel.Password 
            };
        }
        public static RegisterBLModel RegisterToBLModel(this RegisterViewModel registerViewModel)
        {
            return new RegisterBLModel
            {
                Name = registerViewModel.Name,
                Surname = registerViewModel.Surname,
                Phone = registerViewModel.Phone,
                Email = registerViewModel.Email,
                Username = registerViewModel.Username,
                Password = registerViewModel.Password,
            };
        }
    }
}

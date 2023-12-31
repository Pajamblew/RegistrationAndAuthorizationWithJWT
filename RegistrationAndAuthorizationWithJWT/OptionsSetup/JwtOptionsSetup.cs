﻿using Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace RegistrationAndAuthorizationWithJWT.OptionsSetup
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private const string SECTION_NAME = "jwt";
        private readonly IConfiguration _configuration;
        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            _configuration.GetSection(SECTION_NAME).Bind(options);
        }
    }
}

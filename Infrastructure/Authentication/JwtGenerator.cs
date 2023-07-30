using BusinessLogic.Abstractions;
using DBLibrary.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtOptions _options;

        public JwtGenerator(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateJwt(User user)
        {
            var claims = new Claim[] 
            { 
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
            };

            var singingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                null,
                DateTime.UtcNow.AddMinutes(30),
                singingCredentials);

            string? tokenValue = new JwtSecurityTokenHandler()
                .WriteToken(token);
            if (tokenValue == null)
                throw new Exception("Failed to generate token");

            return tokenValue;
        }
    }
}

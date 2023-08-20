using LangBuddy.Authentication.Service.Authentication.Common;
using LangBuddy.Authentication.Service.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LangBuddy.Authentication.Service.Authentication.Commands
{
    public class CreateJwtTokenCommand : ICreateJwtTokenCommand
    {
        private readonly JwtConfiguration _jwtConfiguration;

        public CreateJwtTokenCommand(IOptions<JwtConfiguration> jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration.Value;
        }

        public string Invoke(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtConfiguration.ISSUER,
                Audience = _jwtConfiguration.AUDIENCE,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    _jwtConfiguration.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public string Invoke(string accountEmail, string pasHash)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtConfiguration.ISSUER,
                Audience = _jwtConfiguration.AUDIENCE,
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Email, accountEmail),
                new Claim(ClaimTypes.Hash, pasHash)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    _jwtConfiguration.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}

using LangBuddy.Authentication.Service.Authentication.Common;
using LangBuddy.Authentication.Service.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LangBuddy.Authentication.Service.Authentication.Commands
{
    public class CreateJwtTokenCommand : ICreateJwtTokenCommand
    {
        private readonly JwtConfiguration _jwtConfiguration;

        public CreateJwtTokenCommand(JwtConfiguration jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration;
        }

        public string Invoke(string accountLogin, string pasHash)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtConfiguration.ISSUER,
                Audience = _jwtConfiguration.AUDIENCE,
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, accountLogin),
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

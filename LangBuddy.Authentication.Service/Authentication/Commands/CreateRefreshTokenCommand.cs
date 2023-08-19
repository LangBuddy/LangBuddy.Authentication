using LangBuddy.Authentication.Service.Authentication.Common;
using System.Security.Cryptography;

namespace LangBuddy.Authentication.Service.Authentication.Commands
{
    public class CreateRefreshTokenCommand: ICreateRefreshTokenCommand
    {
        public string Invoke()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}

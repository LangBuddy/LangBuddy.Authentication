using System.Security.Claims;

namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface ICreateJwtTokenCommand
    {
        string Invoke(string accountEmail, string pasHash);
        string Invoke(IEnumerable<Claim> claims);
    }
}

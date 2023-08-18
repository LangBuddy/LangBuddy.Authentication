using LangBuddy.Authentication.Models.Request;

namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface IAuthenticateAccountCommand
    {
        Task<string> Invoke(AuthLoginRequest authLoginRequest);
    }
}

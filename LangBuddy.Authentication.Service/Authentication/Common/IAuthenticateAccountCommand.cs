using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Models.Response;

namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface IAuthenticateAccountCommand
    {
        Task<AuthenticatedResponse> Invoke(AuthLoginRequest authLoginRequest);
    }
}

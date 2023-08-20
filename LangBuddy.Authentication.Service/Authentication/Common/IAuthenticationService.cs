using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Models.Response;

namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface IAuthenticationService
    {
        Task<HttpContent> Register(AuthRegisterRequest authCreateRequest);

        Task<AuthenticatedResponse> Authenticate(AuthLoginRequest authLoginRequest);
    }
}

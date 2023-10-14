using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Models.Response;

namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface IAuthenticationService
    {
        Task Register(AuthRegisterRequest authCreateRequest);

        Task<AuthenticatedResponse> Authenticate(AuthLoginRequest authLoginRequest);

        Task<AuthenticatedResponse> RefreshToken(TokenRefreshRequest tokenRefreshRequest, string email);

        Task Logout(string email);
    }
}

using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Models.Response;
using LangBuddy.Authentication.Models.Responses;

namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface IAuthenticationService
    {
        Task<HttpResponse> Register(AuthRegisterRequest authCreateRequest);

        Task<HttpResponse> Authenticate(AuthLoginRequest authLoginRequest);

        Task<HttpResponse> RefreshToken(TokenRefreshRequest tokenRefreshRequest, string email);

        Task Logout(string email);

        Task<HttpResponse> Profile(string email);
    }
}

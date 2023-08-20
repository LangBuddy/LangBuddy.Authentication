using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Models.Response;

namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface IRefreshTokenCommand
    {
        Task<AuthenticatedResponse> Invoke(TokenRefreshRequest tokenRefreshRequest, string email);
    }
}

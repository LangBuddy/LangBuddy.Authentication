using LangBuddy.Authentication.Models.Request;

namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface IAuthenticationService
    {
        Task<HttpContent> Register(AuthCreateRequest authCreateRequest);
    }
}

using LangBuddy.Authentication.Models.Request;

namespace LangBuddy.Authentication.Service.Http.Common
{
    public interface IHttpService
    {
        Task<HttpContent> SendCreateAccountRequest(AccountCreateRequest accountCreateRequest);
    }
}

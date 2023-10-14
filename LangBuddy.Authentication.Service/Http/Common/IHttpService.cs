using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Models.Response;

namespace LangBuddy.Authentication.Service.Http.Common
{
    public interface IHttpService
    {
        Task<bool> SendCreateAccountRequest(AccountCreateRequest accountCreateRequest);

        Task<AccountPasswordHashResponse?> SendGetAccountPasswordHashRequest(string email);
    }
}

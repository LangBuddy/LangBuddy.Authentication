using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Models.Response;
using LangBuddy.Authentication.Models.Responses;

namespace LangBuddy.Authentication.Service.Http.Common
{
    public interface IHttpService
    {
        Task<AccountCreatedResponse> SendCreateAccountRequest(AccountCreateRequest accountCreateRequest);

        Task<AccountPasswordHashResponse?> SendGetAccountPasswordHashRequest(string email);

        Task<AccountGetByEmailResponse> SendGetAccountByEmail(string email);
    }
}

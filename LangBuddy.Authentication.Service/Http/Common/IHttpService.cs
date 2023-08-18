using LangBuddy.Authentication.Models.Dto;
using LangBuddy.Authentication.Models.Request;

namespace LangBuddy.Authentication.Service.Http.Common
{
    public interface IHttpService
    {
        Task<HttpContent> SendCreateAccountRequest(AccountCreateRequest accountCreateRequest);

        Task<AccountPasswordHashDto?> SendGetAccountPasswordHashRequest(string email);
    }
}

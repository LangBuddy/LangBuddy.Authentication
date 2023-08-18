using LangBuddy.Authentication.Models.Request;

namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface ICreateAccountCommand
    {
        Task<HttpContent> Invoke(AuthCreateRequest authCreateRequest);
    }
}

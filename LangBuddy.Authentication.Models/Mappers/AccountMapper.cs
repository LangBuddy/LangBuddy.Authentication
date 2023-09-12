using LangBuddy.Authentication.Models.Request;
using Mapster;

namespace LangBuddy.Authentication.Models.Mappers
{
    public static class AccountMapper
    {
        public static AccountCreateRequest ToAccountCreateRequest(this AuthRegisterRequest request)
        {
            return request.Adapt<AccountCreateRequest>();
        }
    }
}

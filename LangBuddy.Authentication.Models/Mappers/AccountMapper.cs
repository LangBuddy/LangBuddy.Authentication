using LangBuddy.Authentication.Models.Request;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangBuddy.Authentication.Models.Mappers
{
    public static class AccountMapper
    {
        public static AccountCreateRequest ToAccountCreateRequest(this AuthCreateRequest request)
        {
            var req = request.Adapt<AccountCreateRequest>();
            return request.Adapt<AccountCreateRequest>();
        }
    }
}

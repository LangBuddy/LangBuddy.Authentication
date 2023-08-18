using LangBuddy.Authentication.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface ICreateAccountCommand
    {
        Task<HttpContent> Invoke(AuthCreateRequest authCreateRequest);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangBuddy.Authentication.Models.Request
{
    public record AuthCreateRequest(
        string Email, string Nickname, string Password    
    );
}

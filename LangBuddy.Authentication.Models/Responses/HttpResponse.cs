using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangBuddy.Authentication.Models.Responses
{
    public record HttpResponse(
        bool Success,
        string Message,
        object? Data
    );
}

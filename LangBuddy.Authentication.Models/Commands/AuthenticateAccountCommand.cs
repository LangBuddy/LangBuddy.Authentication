using LangBuddy.Authentication.Models.Response;
using MediatR;

namespace LangBuddy.Authentication.Models.Commands
{
    public record AuthenticateAccountCommand(
        string Email, string Password
    ): IRequest<AuthenticatedResponse>;
}

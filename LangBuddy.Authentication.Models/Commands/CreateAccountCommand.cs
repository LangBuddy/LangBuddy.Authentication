using MediatR;

namespace LangBuddy.Authentication.Models.Commands
{
    public record CreateAccountCommand(
        string Email, string Nickname, string Password
    ): IRequest;
}

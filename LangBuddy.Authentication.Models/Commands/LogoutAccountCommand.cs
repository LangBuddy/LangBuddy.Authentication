using MediatR;

namespace LangBuddy.Authentication.Models.Commands
{
    public record LogoutAccountCommand(string Email): IRequest;
}

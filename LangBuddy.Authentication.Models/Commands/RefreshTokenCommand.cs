using LangBuddy.Authentication.Models.Response;
using MediatR;

namespace LangBuddy.Authentication.Models.Commands
{
    public class RefreshTokenCommand : IRequest<AuthenticatedResponse>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}

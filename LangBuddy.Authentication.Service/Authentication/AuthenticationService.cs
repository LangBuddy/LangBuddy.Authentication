using LangBuddy.Authentication.Models.Commands;
using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Models.Response;
using LangBuddy.Authentication.Service.Authentication.Common;
using LangBuddy.Authentication.Service.Mappers;
using MediatR;

namespace LangBuddy.Authentication.Service.Authentication
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly IMediator _mediator;

        public AuthenticationService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Register(AuthRegisterRequest authCreateRequest)
        {
            await _mediator.Send(authCreateRequest.ToCommand());
        }

        public async Task<AuthenticatedResponse> Authenticate(AuthLoginRequest authLoginRequest)
        {
            return await _mediator.Send(authLoginRequest.ToCommand());
        }

        public async Task<AuthenticatedResponse> RefreshToken(TokenRefreshRequest tokenRefreshRequest, string email)
        {
            return await _mediator.Send(tokenRefreshRequest.ToCommand(email));
        }

        public async Task Logout(string email)
        {
            await _mediator.Send(new LogoutAccountCommand(email));
        }
    }
}

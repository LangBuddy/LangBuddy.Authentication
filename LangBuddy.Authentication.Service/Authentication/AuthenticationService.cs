using LangBuddy.Authentication.Models.Commands;
using LangBuddy.Authentication.Models.Queries;
using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Models.Responses;
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

        public async Task<HttpResponse> Register(AuthRegisterRequest authCreateRequest)
        {
            var res = await _mediator.Send(authCreateRequest.ToCommand());

            return new HttpResponse(true, "Successful registration", res);
        }

        public async Task<HttpResponse> Authenticate(AuthLoginRequest authLoginRequest)
        {
            var res = await _mediator.Send(authLoginRequest.ToCommand());

            return new HttpResponse(true, "Successful authentication", res);
        }

        public async Task<HttpResponse> RefreshToken(TokenRefreshRequest tokenRefreshRequest, string email)
        {
            var res = await _mediator.Send(tokenRefreshRequest.ToCommand(email));

            return new HttpResponse(true, "Successful Refresh Token", res);
        }

        public async Task Logout(string email)
        {
            await _mediator.Send(new LogoutAccountCommand(email));
        }

        public async Task<HttpResponse> Profile(string email)
        {
            var res = await _mediator.Send(new GetAccountQuery(email));

            return new HttpResponse(true, "Successful get account by email", res);
        }
    }
}

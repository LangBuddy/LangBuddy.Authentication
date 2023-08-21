using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Models.Response;
using LangBuddy.Authentication.Service.Authentication.Common;

namespace LangBuddy.Authentication.Service.Authentication
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly ICreateAccountCommand _createAccountCommand;
        private readonly IAuthenticateAccountCommand _authenticateAccountCommand;
        private readonly IRefreshTokenCommand _refreshTokenCommand;
        private readonly IAccountLogoutCommand _accountLogoutCommand;

        public AuthenticationService(ICreateAccountCommand createAccountCommand,
            IAuthenticateAccountCommand authenticateAccountCommand,
            IRefreshTokenCommand refreshTokenCommand,
            IAccountLogoutCommand accountLogoutCommand)
        {
            _createAccountCommand = createAccountCommand;
            _authenticateAccountCommand = authenticateAccountCommand;
            _refreshTokenCommand = refreshTokenCommand;
            _accountLogoutCommand = accountLogoutCommand;
        }

        public async Task<HttpContent> Register(AuthRegisterRequest authCreateRequest)
        {
            return await _createAccountCommand.Invoke(authCreateRequest);
        }

        public async Task<AuthenticatedResponse> Authenticate(AuthLoginRequest authLoginRequest)
        {
            return await _authenticateAccountCommand.Invoke(authLoginRequest);
        }

        public async Task<AuthenticatedResponse> RefreshToken(TokenRefreshRequest tokenRefreshRequest, string email)
        {
            return await _refreshTokenCommand.Invoke(tokenRefreshRequest, email);
        }

        public async Task<int> Logout(string email)
        {
            return await _accountLogoutCommand.Invoke(email);
        }
    }
}

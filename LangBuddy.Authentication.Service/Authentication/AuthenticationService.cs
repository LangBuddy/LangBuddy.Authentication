using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Service.Authentication.Common;

namespace LangBuddy.Authentication.Service.Authentication
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly ICreateAccountCommand _createAccountCommand;
        private readonly IAuthenticateAccountCommand _authenticateAccountCommand;

        public AuthenticationService(ICreateAccountCommand createAccountCommand,
            IAuthenticateAccountCommand authenticateAccountCommand)
        {
            _createAccountCommand = createAccountCommand;
            _authenticateAccountCommand = authenticateAccountCommand;
        }

        public async Task<HttpContent> Register(AuthRegisterRequest authCreateRequest)
        {
            return await _createAccountCommand.Invoke(authCreateRequest);
        }

        public async Task<string> Authenticate(AuthLoginRequest authLoginRequest)
        {
            return await _authenticateAccountCommand.Invoke(authLoginRequest);
        }
    }
}

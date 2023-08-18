using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Service.Authentication.Common;

namespace LangBuddy.Authentication.Service.Authentication
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly ICreateAccountCommand _createAccountCommand;

        public AuthenticationService(ICreateAccountCommand createAccountCommand)
        {
            _createAccountCommand = createAccountCommand;
        }

        public async Task<HttpContent> Register(AuthRegisterRequest authCreateRequest)
        {
            return await _createAccountCommand.Invoke(authCreateRequest);
        }
    }
}

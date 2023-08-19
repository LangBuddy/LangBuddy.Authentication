using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Models.Response;
using LangBuddy.Authentication.Service.Authentication.Common;
using LangBuddy.Authentication.Service.Http.Common;

namespace LangBuddy.Authentication.Service.Authentication.Commands
{
    public class AuthenticateAccountCommand : IAuthenticateAccountCommand
    {
        private readonly IVerifyPasswordHashCommand _verifyPasswordHashCommand;
        private readonly ICreateJwtTokenCommand _createJwtTokenCommand;
        private readonly ICreateRefreshTokenCommand _createRefreshTokenCommand;
        private readonly IHttpService _httpService;

        public AuthenticateAccountCommand(IVerifyPasswordHashCommand verifyPasswordHashCommand,
            ICreateJwtTokenCommand createJwtTokenCommand,
            ICreateRefreshTokenCommand createRefreshTokenCommand,
            IHttpService httpService)
        {
            _verifyPasswordHashCommand = verifyPasswordHashCommand;
            _createJwtTokenCommand = createJwtTokenCommand;
            _createRefreshTokenCommand = createRefreshTokenCommand;
            _httpService = httpService;
        }

        public async Task<AuthenticatedResponse> Invoke(AuthLoginRequest authLoginRequest)
        {
            var passwordHash = await _httpService.SendGetAccountPasswordHashRequest(authLoginRequest.Email);
            var verifyPassword = _verifyPasswordHashCommand.Invoke(
                authLoginRequest.Password, passwordHash.PasswordHash, passwordHash.PasswordSalt
            );

            if (!verifyPassword)
            {
                throw new Exception("Password Incorrect");
            }

            var token = _createJwtTokenCommand.Invoke(authLoginRequest.Email,
                System.Text.Encoding.UTF8.GetString(passwordHash.PasswordHash));

            var refresh = _createRefreshTokenCommand.Invoke();

            return new AuthenticatedResponse(token, refresh);
        }
    }
}

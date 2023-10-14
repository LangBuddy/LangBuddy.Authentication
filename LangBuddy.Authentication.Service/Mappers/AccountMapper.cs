using LangBuddy.Authentication.Models.Commands;
using LangBuddy.Authentication.Models.Request;
using Mapster;
using MediatR;

namespace LangBuddy.Authentication.Service.Mappers
{
    public static class AccountMapper
    {
        public static AccountCreateRequest ToAccountCreateRequest(this AuthRegisterRequest request)
        {
            return request.Adapt<AccountCreateRequest>();
        }

        public static AccountCreateRequest ToAccountCreateRequest(this CreateAccountCommand request, byte[] passwordHash, byte[] passwordSalt)
        {
            var accountCreateRequest = request.Adapt<AccountCreateRequest>();
            accountCreateRequest.PasswordHash = passwordHash;
            accountCreateRequest.PasswordSalt = passwordSalt;

            return accountCreateRequest;
        }

        public static CreateAccountCommand ToCommand(this AuthRegisterRequest authRegisterRequest)
        {
            return authRegisterRequest.Adapt<CreateAccountCommand>();
        }

        public static AuthenticateAccountCommand ToCommand(this AuthLoginRequest authLoginRequest)
        {
            return authLoginRequest.Adapt<AuthenticateAccountCommand>();
        }
        public static RefreshTokenCommand ToCommand(this TokenRefreshRequest authLoginRequest, string email)
        {
            var authenticateAccountCommand = authLoginRequest.Adapt<RefreshTokenCommand>();
            authenticateAccountCommand.Email = email;

            return authenticateAccountCommand;
        }
    }
}

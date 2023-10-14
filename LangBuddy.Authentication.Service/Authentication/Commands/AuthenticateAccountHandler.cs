using LangBuddy.Authentication.Database;
using LangBuddy.Authentication.Models.Commands;
using LangBuddy.Authentication.Models.Response;
using LangBuddy.Authentication.Service.Authentication.Common;
using LangBuddy.Authentication.Service.Http.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace LangBuddy.Authentication.Service.Authentication.Commands
{
    public class AuthenticateAccountHandler : IRequestHandler<AuthenticateAccountCommand, AuthenticatedResponse>
    {
        private readonly AuthenticationDbContext _authenticationDbContext;
        private readonly ICreateJwtTokenCommand _createJwtTokenCommand;
        private readonly ICreateRefreshTokenCommand _createRefreshTokenCommand;
        private readonly IHttpService _httpService;

        public AuthenticateAccountHandler(AuthenticationDbContext authenticationDbContext,
            ICreateJwtTokenCommand createJwtTokenCommand,
            ICreateRefreshTokenCommand createRefreshTokenCommand,
            IHttpService httpService)
        {
            _authenticationDbContext = authenticationDbContext;
            _createJwtTokenCommand = createJwtTokenCommand;
            _createRefreshTokenCommand = createRefreshTokenCommand;
            _httpService = httpService;
        }

        public async Task<AuthenticatedResponse> Handle(AuthenticateAccountCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = await _httpService.SendGetAccountPasswordHashRequest(request.Email);
            var verifyPassword = VerifyPasswordHash(
                request.Password, passwordHash.PasswordHash, passwordHash.PasswordSalt
            );

            if (!verifyPassword)
            {
                throw new Exception("Password Incorrect");
            }

            var token = _createJwtTokenCommand.Invoke(request.Email,
                System.Text.Encoding.UTF8.GetString(passwordHash.PasswordHash));

            var refresh = _createRefreshTokenCommand.Invoke();

            var auth = await _authenticationDbContext.Authentications
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (auth is null)
            {
                auth = new Database.Entity.Authentication()
                {
                    Email = request.Email,
                    RefreshToken = refresh,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7),
                };

                await _authenticationDbContext.Authentications.AddAsync(auth);
            }
            else
            {
                auth.RefreshToken = refresh;
                auth.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            }

            await _authenticationDbContext.SaveChangesAsync();

            return new AuthenticatedResponse(token, refresh);
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");

            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordSalt");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }

            return true;
        }
    }
}

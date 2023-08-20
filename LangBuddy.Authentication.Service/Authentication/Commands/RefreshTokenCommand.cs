using LangBuddy.Authentication.Database;
using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Models.Response;
using LangBuddy.Authentication.Service.Authentication.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace LangBuddy.Authentication.Service.Authentication.Commands
{
    public class RefreshTokenCommand : IRefreshTokenCommand
    {
        private readonly AuthenticationDbContext _authenticationDbContext;
        private readonly IGetPrincipalFromExpiredTokenCommand _getPrincipalFromExpiredTokenCommand;
        private readonly ICreateJwtTokenCommand _createJwtTokenCommand;
        private readonly ICreateRefreshTokenCommand _createRefreshTokenCommand;

        public RefreshTokenCommand(AuthenticationDbContext authenticationDbContext,
            IGetPrincipalFromExpiredTokenCommand getPrincipalFromExpiredTokenCommand,
            ICreateJwtTokenCommand createJwtTokenCommand,
            ICreateRefreshTokenCommand createRefreshTokenCommand)
        {
            _authenticationDbContext = authenticationDbContext;
            _getPrincipalFromExpiredTokenCommand = getPrincipalFromExpiredTokenCommand;
            _createJwtTokenCommand = createJwtTokenCommand;
            _createRefreshTokenCommand = createRefreshTokenCommand;
        }

        public async Task<AuthenticatedResponse> Invoke(TokenRefreshRequest tokenRefreshRequest, string email)
        {
            var principal = _getPrincipalFromExpiredTokenCommand.Invoke(tokenRefreshRequest.Token);
            var user = await _authenticationDbContext.Authentications
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null
                || user.RefreshToken != tokenRefreshRequest.RefreshToken
                || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new ArgumentException("Invalid client request");

            var newToken = _createJwtTokenCommand.Invoke(principal.Claims);
            var newRefresh = _createRefreshTokenCommand.Invoke();

            user.RefreshToken = newRefresh;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _authenticationDbContext.SaveChangesAsync();

            return new AuthenticatedResponse(newToken, newRefresh);
        }
    }
}

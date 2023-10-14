using LangBuddy.Authentication.Database;
using LangBuddy.Authentication.Models.Response;
using LangBuddy.Authentication.Service.Authentication.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LangBuddy.Authentication.Service.Authentication.Commands
{
    public class RefreshTokenHandler : IRequestHandler<Models.Commands.RefreshTokenCommand, AuthenticatedResponse>
    {
        private readonly AuthenticationDbContext _authenticationDbContext;
        private readonly IGetPrincipalFromExpiredTokenCommand _getPrincipalFromExpiredTokenCommand;
        private readonly ICreateJwtTokenCommand _createJwtTokenCommand;
        private readonly ICreateRefreshTokenCommand _createRefreshTokenCommand;

        public RefreshTokenHandler(AuthenticationDbContext authenticationDbContext,
            IGetPrincipalFromExpiredTokenCommand getPrincipalFromExpiredTokenCommand,
            ICreateJwtTokenCommand createJwtTokenCommand,
            ICreateRefreshTokenCommand createRefreshTokenCommand)
        {
            _authenticationDbContext = authenticationDbContext;
            _getPrincipalFromExpiredTokenCommand = getPrincipalFromExpiredTokenCommand;
            _createJwtTokenCommand = createJwtTokenCommand;
            _createRefreshTokenCommand = createRefreshTokenCommand;
        }

        public async Task<AuthenticatedResponse> Handle(Models.Commands.RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _getPrincipalFromExpiredTokenCommand.Invoke(request.Token);
            var user = await _authenticationDbContext.Authentications
            .FirstOrDefaultAsync(x => x.Email.Equals(request.Email));

            if (user is null
                || user.RefreshToken != request.RefreshToken
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

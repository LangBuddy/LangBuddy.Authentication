using LangBuddy.Authentication.Database;
using LangBuddy.Authentication.Models.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LangBuddy.Authentication.Service.Authentication.Commands
{
    public class LogoutAccountHandler : IRequestHandler<LogoutAccountCommand>
    {
        private readonly AuthenticationDbContext _authenticationDbContext;

        public LogoutAccountHandler(AuthenticationDbContext authenticationDbContext)
        {
            _authenticationDbContext = authenticationDbContext;
        }

        public async Task Handle(LogoutAccountCommand request, CancellationToken cancellationToken)
        {
            var authenticate = await _authenticationDbContext.Authentications
                .FirstOrDefaultAsync(el => el.Email.Equals(request.Email));

            _authenticationDbContext.Remove(authenticate);

            await _authenticationDbContext.SaveChangesAsync();
        }
    }
}

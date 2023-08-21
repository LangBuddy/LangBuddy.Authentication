using LangBuddy.Authentication.Database;
using LangBuddy.Authentication.Service.Authentication.Common;
using Microsoft.EntityFrameworkCore;

namespace LangBuddy.Authentication.Service.Authentication.Commands
{
    public class AccountLogoutCommand : IAccountLogoutCommand
    {
        private readonly AuthenticationDbContext _authenticationDbContext;

        public AccountLogoutCommand(AuthenticationDbContext authenticationDbContext)
        {
            _authenticationDbContext = authenticationDbContext;
        }

        public async Task<int> Invoke(string email)
        {
            var authenticate = await _authenticationDbContext.Authentications
                .FirstOrDefaultAsync(el => el.Email.Equals(email));

            _authenticationDbContext.Remove(authenticate);

            return await _authenticationDbContext.SaveChangesAsync();
        }
    }
}

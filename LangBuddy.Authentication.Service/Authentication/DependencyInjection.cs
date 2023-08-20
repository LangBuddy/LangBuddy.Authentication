using LangBuddy.Authentication.Service.Authentication.Commands;
using LangBuddy.Authentication.Service.Authentication.Common;
using Microsoft.Extensions.DependencyInjection;

namespace LangBuddy.Authentication.Service.Authentication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
        {
            services.AddTransient<ICreatePasswordHashCommand, CreatePasswordHashCommand>();
            services.AddTransient<IVerifyPasswordHashCommand, VerifyPasswordHashCommand>();
            services.AddTransient<ICreateJwtTokenCommand, CreateJwtTokenCommand>();
            services.AddTransient<ICreateRefreshTokenCommand, CreateRefreshTokenCommand>();
            services.AddTransient<IGetPrincipalFromExpiredTokenCommand, GetPrincipalFromExpiredTokenCommand>();
            services.AddTransient<ICreateAccountCommand, CreateAccountCommand>();
            services.AddTransient<IAuthenticateAccountCommand, AuthenticateAccountCommand>();
            services.AddTransient<IRefreshTokenCommand, RefreshTokenCommand>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}

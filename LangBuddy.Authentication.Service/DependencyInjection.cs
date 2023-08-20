using LangBuddy.Authentication.Database;
using LangBuddy.Authentication.Service.Authentication;
using LangBuddy.Authentication.Service.Authentication.Commands;
using LangBuddy.Authentication.Service.Authentication.Common;
using LangBuddy.Authentication.Service.Http;
using LangBuddy.Authentication.Service.Http.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LangBuddy.Authentication.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddTransient<IHttpService, HttpService>();

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
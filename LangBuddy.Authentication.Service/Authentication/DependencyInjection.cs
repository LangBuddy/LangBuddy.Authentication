using LangBuddy.Authentication.Service.Authentication.Commands;
using LangBuddy.Authentication.Service.Authentication.Common;
using LangBuddy.Authentication.Service.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LangBuddy.Authentication.Service.Authentication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfiguration = configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
            services.Configure<JwtConfiguration>(el => configuration.GetSection("JwtConfiguration").Bind(el));

            services.AddAuthorization();
            services.AddAuthentication(options =>
            {

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    // будет ли валидироваться потребитель токена
                    ValidateAudience = true,
                    // будет ли валидироваться время существования
                    ValidateLifetime = true,
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,
                    // строка, представляющая издателя
                    ValidIssuer = jwtConfiguration.ISSUER,
                    // установка потребителя токена
                    ValidAudience = jwtConfiguration.AUDIENCE,
                    // установка ключа безопасности
                    IssuerSigningKey = jwtConfiguration.GetSymmetricSecurityKey(),
                };
            });

            services.AddTransient<ICreatePasswordHashCommand, CreatePasswordHashCommand>();
            services.AddTransient<IVerifyPasswordHashCommand, VerifyPasswordHashCommand>();
            services.AddTransient<ICreateJwtTokenCommand, CreateJwtTokenCommand>();
            services.AddTransient<ICreateRefreshTokenCommand, CreateRefreshTokenCommand>();
            services.AddTransient<IGetPrincipalFromExpiredTokenCommand, GetPrincipalFromExpiredTokenCommand>();
            services.AddTransient<ICreateAccountCommand, CreateAccountCommand>();
            services.AddTransient<IAuthenticateAccountCommand, AuthenticateAccountCommand>();
            services.AddTransient<IRefreshTokenCommand, RefreshTokenCommand>();
            services.AddTransient<IAccountLogoutCommand, AccountLogoutCommand>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}

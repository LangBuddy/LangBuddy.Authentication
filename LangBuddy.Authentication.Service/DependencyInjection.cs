using LangBuddy.Authentication.Database;
using LangBuddy.Authentication.Service.Authentication;
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
            services.AddAuthenticationService(configuration);

            return services;
        }
    }
}
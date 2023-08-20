using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LangBuddy.Authentication.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AuthenticationDbContext>(options =>
            {
                options.UseNpgsql(connectionString,
                    options => options.UseNodaTime());
            });

            services.AddScoped<AuthenticationDbContext>();

            return services;
        }
    }
}

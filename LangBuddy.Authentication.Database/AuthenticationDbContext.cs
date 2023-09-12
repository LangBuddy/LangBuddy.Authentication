using Microsoft.EntityFrameworkCore;

namespace LangBuddy.Authentication.Database
{
    public class AuthenticationDbContext: DbContext
    {
        public DbSet<Entity.Authentication> Authentications { get; set; }

        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options): base(options) 
        {
            Database.MigrateAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
namespace LangBuddy.Authentication.Database.Entity
{
    public class Authentication
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

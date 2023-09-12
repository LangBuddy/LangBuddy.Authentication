namespace LangBuddy.Authentication.Models.Request
{
    public record TokenRefreshRequest(string Token, string RefreshToken);
}

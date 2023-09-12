namespace LangBuddy.Authentication.Models.Request
{
    public record AuthLoginRequest(
        string Email, string Password
    );
}

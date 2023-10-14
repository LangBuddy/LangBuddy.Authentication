namespace LangBuddy.Authentication.Models.Response
{
    public record AuthenticatedResponse(string Token, string RefreshToken);
}

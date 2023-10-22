namespace LangBuddy.Authentication.Models.Responses
{
    public record AccountGetByEmailResponse(
        string Email,
        string Nickname,
        long? UserId
    );
}

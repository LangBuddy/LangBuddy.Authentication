namespace LangBuddy.Authentication.Models.Response
{
    public record AccountPasswordHashResponse(
        byte[] PasswordSalt, byte[] PasswordHash
    );
}

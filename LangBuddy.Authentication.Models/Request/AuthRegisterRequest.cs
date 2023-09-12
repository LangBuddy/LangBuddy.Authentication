namespace LangBuddy.Authentication.Models.Request
{
    public record AuthRegisterRequest(
        string Email, string Nickname, string Password    
    );
}

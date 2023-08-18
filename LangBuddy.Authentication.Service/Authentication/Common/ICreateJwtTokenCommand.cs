namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface ICreateJwtTokenCommand
    {
        string Invoke(string accountLogin, string pasHash);
    }
}

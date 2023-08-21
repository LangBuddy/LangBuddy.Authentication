namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface IAccountLogoutCommand
    {
        Task<int> Invoke(string email);
    }
}

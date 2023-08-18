namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface ICreatePasswordHashCommand
    {
        void Invoke(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}

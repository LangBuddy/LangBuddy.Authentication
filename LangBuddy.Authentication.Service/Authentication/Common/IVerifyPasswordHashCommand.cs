namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface IVerifyPasswordHashCommand
    {
        bool Invoke(string password, byte[] storedHash, byte[] storedSalt);
    }
}

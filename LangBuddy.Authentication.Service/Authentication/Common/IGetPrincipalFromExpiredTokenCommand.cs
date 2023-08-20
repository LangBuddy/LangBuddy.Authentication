namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface IGetPrincipalFromExpiredTokenCommand
    {
        System.Security.Claims.ClaimsPrincipal Invoke(string token);
    }
}

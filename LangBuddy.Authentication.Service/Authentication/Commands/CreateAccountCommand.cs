using LangBuddy.Authentication.Models.Mappers;
using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Service.Authentication.Common;
using LangBuddy.Authentication.Service.Http.Common;

namespace LangBuddy.Authentication.Service.Authentication.Commands
{
    public class CreateAccountCommand: ICreateAccountCommand
    {
        private readonly ICreatePasswordHashCommand _createPasswordHashCommand;
        private readonly IHttpService _httpService;

        public CreateAccountCommand(ICreatePasswordHashCommand createPasswordHashCommand,
            IHttpService httpService)
        {
            _createPasswordHashCommand = createPasswordHashCommand;
            _httpService = httpService;
        }
        public async Task<HttpContent> Invoke(AuthCreateRequest authCreateRequest)
        {
            byte[] passwordHash, passwordSalt;
            _createPasswordHashCommand.Invoke(authCreateRequest.Password, out passwordHash, out passwordSalt);

            var account = authCreateRequest.ToAccountCreateRequest();

            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;

            return await _httpService.SendCreateAccountRequest(account);
        }
    }
}

using LangBuddy.Authentication.Models.Queries;
using LangBuddy.Authentication.Models.Responses;
using LangBuddy.Authentication.Service.Http.Common;
using MediatR;

namespace LangBuddy.Authentication.Service.Authentication.Queries
{
    public class GetAccountHandler : IRequestHandler<GetAccountQuery, AccountGetByEmailResponse>
    {
        private readonly IHttpService _httpService;

        public GetAccountHandler(IHttpService httpService) 
        {
            _httpService = httpService;
        }

        public async Task<AccountGetByEmailResponse> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var res = await _httpService.SendGetAccountByEmail(request.Email);
            return res;
        }
    }
}

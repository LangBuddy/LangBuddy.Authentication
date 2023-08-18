using LangBuddy.Authentication.Models.Options;
using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Service.Http.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace LangBuddy.Authentication.Service.Http
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConnections _apiConnections;

        public HttpService(HttpClient httpClient, 
            IOptions<ApiConnections> apiConnections)
        {
            _httpClient = httpClient;
            _apiConnections = apiConnections.Value;
        }

        public async Task<HttpContent> SendCreateAccountRequest(AccountCreateRequest accountCreateRequest)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _apiConnections.AccountsConnection);

            request.Content = new StringContent(
                JsonConvert.SerializeObject(accountCreateRequest), Encoding.UTF8, "application/json"
            );

            var response = await _httpClient.SendAsync(request);

            return response.Content;
        }
    }
}

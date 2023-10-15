using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Models.Response;
using LangBuddy.Authentication.Models.Responses;
using LangBuddy.Authentication.Service.Http.Common;
using LangBuddy.Authentication.Service.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace LangBuddy.Authentication.Service.Http
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConnections _apiConnections;

        public HttpService(IOptions<ApiConnections> apiConnections)
        {
            _httpClient = new HttpClient();
            _apiConnections = apiConnections.Value;
        }

        public async Task<AccountCreatedResponse> SendCreateAccountRequest(AccountCreateRequest accountCreateRequest)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _apiConnections.AccountsConnectionDefault);

            request.Content = new StringContent(
                JsonConvert.SerializeObject(accountCreateRequest), Encoding.UTF8, "application/json"
            );

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<HttpResponse>();

                if (content.Success)
                {
                    var accountCreatedResponse = JsonConvert.DeserializeObject<AccountCreatedResponse>(content.Data.ToString());
                    return accountCreatedResponse;
                }

                throw new Exception(content.Message);
            }

            var content2 = await response.Content.ReadFromJsonAsync<HttpResponse>();
            throw new Exception($"Error when creating an account {content2.Message}");
        }

        public async Task<AccountPasswordHashResponse> SendGetAccountPasswordHashRequest(string email)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                $"{_apiConnections.AccountsConnectionDefaultPasswordHash}/{email}"    
            );

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<HttpResponse>();

                if (content.Success)
                {
                    var accountPasswordHashResponse = JsonConvert.DeserializeObject<AccountPasswordHashResponse>(content.Data.ToString());
                    return accountPasswordHashResponse;
                }

                throw new Exception(content.Message);
            }

            throw new Exception($"Error when receiving the password hash for email {email}. Error code: {response.StatusCode}");
        }
    }
}

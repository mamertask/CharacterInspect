using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using CharacterInspect.Services.Contracts;

namespace CharacterInspect.Services
{
    public class BlizzardApiAuthService : IApiAuthService
    {
        private readonly IClientCredentials _credentials;
        private readonly IHttpClientFactory _httpClientFactory;
        private string _accessToken;

        public BlizzardApiAuthService(IClientCredentials credentials, IHttpClientFactory httpClientFactory)
        {
            _credentials = credentials;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if(_accessToken != null)
            {
                return _accessToken;
            }

            using var httpClient = _httpClientFactory.CreateClient();

            var authRequest = new HttpRequestMessage(HttpMethod.Post, "https://oauth.battle.net/token");

            authRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_credentials.GetClientId()}:{_credentials.GetClientSecret()}")));

            authRequest.Content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("grant_type", "client_credentials") });

            var authResponse = await httpClient.SendAsync(authRequest);
            if (authResponse.IsSuccessStatusCode)
            {
                var authContent = await authResponse.Content.ReadAsStringAsync();
                _accessToken = JObject.Parse(authContent)["access_token"].ToString();
                return _accessToken;
            }
            else 
            {
                Console.WriteLine($"Failed to get access token. StatusCode: {authResponse.StatusCode}, ReasonPhrase: {authResponse.ReasonPhrase}");
            }

            return null;
        }
    }
}

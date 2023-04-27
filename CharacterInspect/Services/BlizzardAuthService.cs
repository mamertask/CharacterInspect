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
    public class BlizzardAuthService : IAuthService
    {
        private readonly IClientCredentials _credentials;
        private string _accessToken;

        BlizzardAuthService(IClientCredentials credentials)
        {
            _credentials = credentials;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if(_accessToken != null)
            {
                return _accessToken;
            }

            using var httpClient = new HttpClient();

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

            return null;
        }
    }
}

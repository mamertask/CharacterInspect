using CharacterInspect.Services.Contracts;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace CharacterInspect.Services
{
    public class BlizzardApiService : IBlizzardApiService
    {
        private readonly IApiAuthService _authenticator;
        private readonly IHttpClientFactory _httpClientFactory;

        public BlizzardApiService(IApiAuthService authenticator, IHttpClientFactory httpClientFactory)
        {
            _authenticator = authenticator;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<JObject> GetCharacterProfileAsync(string region, string realm, string characterName)
        {
            string accessToken = await _authenticator.GetAccessTokenAsync();
            if(accessToken == null) 
            {
                Console.WriteLine("Failed to access token");
                return null;
            }

            using var httpClient = _httpClientFactory.CreateClient();

            // region name lower case/upper case (us.api..., ...=en-US)
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://{region}.api.blizzard.com/profile/wow/character/{realm}/{characterName}?namespace=profile-us&locale=en_{region}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JObject.Parse(content);
            }
            else
            {
                Console.WriteLine($"Failed to fetch character profile data. \n Status code:{response.StatusCode} \n ReasonResponse: {response.ReasonPhrase}");
            }

            return null;
        }
    }
}

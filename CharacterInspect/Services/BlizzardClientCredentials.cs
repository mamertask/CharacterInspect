using CharacterInspect.Services.Contracts;

namespace CharacterInspect.Services
{
    public class BlizzardClientCredentials : IClientCredentials
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public BlizzardClientCredentials(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public string GetClientId()
        {
            return _clientId;
        }

        public string GetClientSecret()
        {
            return _clientSecret;
        }
    }
}

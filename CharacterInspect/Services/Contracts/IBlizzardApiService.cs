using Newtonsoft.Json.Linq;

namespace CharacterInspect.Services.Contracts
{
    public interface IBlizzardApiService
    {
        Task<JObject> GetCharacterProfileAsync(string region, string realm, string characterName);
    }
}

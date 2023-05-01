using Newtonsoft.Json.Linq;

namespace CharacterInspect.Services.Contracts
{
    public interface ICharacterService
    {
        Task<JObject> GetCharacterProfileAsync(string realm, string characterName);
    }
}

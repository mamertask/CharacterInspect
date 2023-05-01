namespace CharacterInspect.Services.Contracts
{
    public interface IApiAuthService
    {
        Task<string> GetAccessTokenAsync();
    }
}

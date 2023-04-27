namespace CharacterInspect.Services.Contracts
{
    public interface IAuthService
    {
        Task<string> GetAccessTokenAsync();
    }
}

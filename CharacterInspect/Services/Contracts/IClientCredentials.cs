namespace CharacterInspect.Services.Contracts
{
    public interface IClientCredentials
    {
        string GetClientId();
        string GetClientSecret();
    }
}

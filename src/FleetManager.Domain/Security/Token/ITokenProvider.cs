namespace FleetManager.Domain.Security.Token
{
    public interface ITokenProvider
    {
        string TokenOnRequest();
    }
}

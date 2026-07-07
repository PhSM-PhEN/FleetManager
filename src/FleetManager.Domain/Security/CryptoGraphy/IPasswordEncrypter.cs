namespace FleetManager.Domain.Security.CryptoGraphy
{
    public interface IPasswordEncrypter
    {
        string Encrypt(string password);
        bool Verify(string password, string hashedPassword);
    }
}

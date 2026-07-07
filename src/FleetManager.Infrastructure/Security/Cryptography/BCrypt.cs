using FleetManager.Domain.Security.CryptoGraphy;
using BC = BCrypt.Net.BCrypt;

namespace FleetManager.Infrastructure.Security.Cryptography
{
    public class BCrypt : IPasswordEncrypter
    {
        public string Encrypt(string password)
        {
            string passwordHash = BC.HashPassword(password);
            return passwordHash;
        }

        public bool Verify(string password, string hashedPassword)
        {
            return BC.Verify(password, hashedPassword);
        }
    }
}   

       

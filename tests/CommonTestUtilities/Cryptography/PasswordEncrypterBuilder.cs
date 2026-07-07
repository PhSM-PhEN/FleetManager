using FleetManager.Domain.Security.CryptoGraphy;
using Moq;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncrypterBuilder 
    {
        private readonly Mock<IPasswordEncrypter> _mock;

        public PasswordEncrypterBuilder()
        {
            _mock = new Mock<IPasswordEncrypter>();
            _mock.Setup(password => password
            .Encrypt(It.IsAny<string>()))
                 .Returns("encryptedPassword12");
        }

        public PasswordEncrypterBuilder Verify(string? password)
        {
            if (string.IsNullOrEmpty(password) == false)
            {
                _mock.Setup(passwordEncrypter => passwordEncrypter.Verify(password, It.IsAny<string>()))
                     .Returns(true);
            }
            return this;
        }

        public IPasswordEncrypter Build()
        {
            return _mock.Object;
        }
    }
}

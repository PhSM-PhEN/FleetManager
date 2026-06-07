using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToUser;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private readonly Mock<IUserReadOnlyRepository> _repository;

        public UserReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IUserReadOnlyRepository>();
        }
        public void ExistsByEmail(string email)
        {
            _repository.Setup(r => r.ExistByEmail(email)).ReturnsAsync(true);
        }

        public UserReadOnlyRepositoryBuilder GetUserByEmail(User user)
        {
            _repository.Setup(r => r.GetUserByEmail(user.Email)).ReturnsAsync(user);
            return this;

        }
        public IUserReadOnlyRepository Build() => _repository.Object;
    }
}

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
        public void ExistByEmail(string email)
        {
            _repository.Setup(user => user.ExistByEmail(email)).ReturnsAsync(true);
        }
        public UserReadOnlyRepositoryBuilder GetUserByEmail(User user)
        {
            _repository.Setup(u => u.GetUserByEmail(user.Email)).ReturnsAsync(user);
            return this;
        }
        public UserReadOnlyRepositoryBuilder GetUserByEmail(string email, User? user)
        {
            _repository.Setup(u => u.GetUserByEmail(email)).ReturnsAsync(user);
            return this;
        }
        public UserReadOnlyRepositoryBuilder GetById(User user)
        {
            _repository.Setup(u => u.GetUserById(user.Id)).ReturnsAsync(user);
            return this;
        }
        public UserReadOnlyRepositoryBuilder ExistsByRole(string role, bool exists)
        {
            _repository.Setup(u => u.ExistsByRole(role)).ReturnsAsync(exists);
            return this;
        }
        public IUserReadOnlyRepository Build()
        {
            return _repository.Object;
        }

    }
}
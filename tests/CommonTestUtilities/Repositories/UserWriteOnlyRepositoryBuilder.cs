using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToUser;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UserWriteOnlyRepositoryBuilder
    {
        private readonly Mock<IUserWriteOnlyRepository> _repository;

        public UserWriteOnlyRepositoryBuilder()
        {
            _repository = new Mock<IUserWriteOnlyRepository>();
        }

        public UserWriteOnlyRepositoryBuilder Add(User user)
        {
            _repository.Setup(r => r.Add(user)).Returns(Task.CompletedTask);
            return this;
        }

        public UserWriteOnlyRepositoryBuilder Delete(User user)
        {
            _repository.Setup(r => r.Delete(user)).Returns(Task.CompletedTask);
            return this;
        }

        public UserWriteOnlyRepositoryBuilder GetUserById(User user)
        {
            _repository.Setup(r => r.GetUserById(user.Id)).ReturnsAsync(user);
            return this;
        }

        public UserWriteOnlyRepositoryBuilder Update(User user)
        {
            _repository.Setup(r => r.Update(user));
            return this;
        }

        public IUserWriteOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
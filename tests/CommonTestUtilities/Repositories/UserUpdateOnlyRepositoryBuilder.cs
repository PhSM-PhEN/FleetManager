using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToUser;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UserUpdateOnlyRepositoryBuilder
    {
        public static IUserUpdateOnlyRepository Build(User user)
        {
            var mock = new Mock<IUserUpdateOnlyRepository>();
            mock.Setup(repo => repo.GetById(user.Id)).ReturnsAsync(user);

            return mock.Object;
        }
    }
}

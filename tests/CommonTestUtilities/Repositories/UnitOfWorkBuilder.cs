using FleetManager.Domain.Repositories;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UnitOfWorkBuilder
    {
        public static IUnitOfWork Build()
        {
            var mock = new Mock<IUnitOfWork>();

            return mock.Object;
        }

        public static IUnitOfWork BuildThrowingOnCommit(Exception exception)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.Commit()).ThrowsAsync(exception);

            return mock.Object;
        }
    }
}

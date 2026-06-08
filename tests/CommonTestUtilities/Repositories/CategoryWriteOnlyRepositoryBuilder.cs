using FleetManager.Domain.Repositories.ToCategory;
using Moq;

namespace CommonTestUtilities.Repositories;

public class CategoryWriteOnlyRepositoryBuilder
{
    public static ICategoryWriteOnlyRepository Builder()
    {
        var mock = new Mock<ICategoryWriteOnlyRepository>();
        return mock.Object;
    }
}

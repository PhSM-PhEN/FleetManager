using FleetManager.Domain.Repositories.ToCompany;
using Moq;

namespace CommonTestUtilities.Repositories;

public class CompanyWriteOnlyRepositoryBuilder
{
    public static ICompanyWriteOnlyRepository Build()
    {
        var mock = new Mock<ICompanyWriteOnlyRepository>();

        return mock.Object;
    }
}

using System;
using FleetManager.Domain.Repositories.ToRental;
using Moq;

namespace CommonTestUtilities.Repositories;

public class RentalWriteOnlyRepositoryBuilder
{
    public IRentalWriteOnlyRepository Build()
    {
        var mock = new Mock<IRentalWriteOnlyRepository>();
        return mock.Object;
    }
}

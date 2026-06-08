using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRental;
using Moq;

namespace CommonTestUtilities.Repositories;

public class RentalUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IRentalUpdateOnlyRepository> _repository;

    public RentalUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<IRentalUpdateOnlyRepository>();
    }
    public RentalUpdateOnlyRepositoryBuilder GetById(Rental rental)
    {
        _repository.Setup(r => r.GetById(rental.Id)).ReturnsAsync(rental);
        return this;
    }

    public IRentalUpdateOnlyRepository Build() => _repository.Object;

}

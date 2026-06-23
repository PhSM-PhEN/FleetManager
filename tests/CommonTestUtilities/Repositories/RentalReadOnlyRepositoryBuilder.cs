using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRental;
using Moq;

namespace CommonTestUtilities.Repositories;

public class RentalReadOnlyRepositoryBuilder
{
    private readonly Mock<IRentalReadOnlyRepository> _repository;

    public RentalReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IRentalReadOnlyRepository>();
    }
    public RentalReadOnlyRepositoryBuilder GetById(Rental rental)
    {
        _repository.Setup(r => r.GetById(rental.Id)).ReturnsAsync(rental);
        return this;
    }
    public RentalReadOnlyRepositoryBuilder GetAll(List<Rental> rentals, int pageNumber = 1, int pageSize = 10)
    {
        _repository.Setup(r => r.GetAll(pageNumber, pageSize)).ReturnsAsync((rentals, rentals.Count));
        return this;
    }

    public IRentalReadOnlyRepository Build() => _repository.Object;

}

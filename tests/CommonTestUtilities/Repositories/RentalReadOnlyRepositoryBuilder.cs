using System;
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
    public RentalReadOnlyRepositoryBuilder GetAll(List<Rental> rentals)
    {
        _repository.Setup(r => r.GetAll()).ReturnsAsync(rentals);
        return this;
    }

    public IRentalReadOnlyRepository Build() => _repository.Object;

}

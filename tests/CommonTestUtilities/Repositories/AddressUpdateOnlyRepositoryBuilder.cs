using System;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToAddress;
using Moq;

namespace CommonTestUtilities.Repositories;

public class AddressUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IAddressUpdateOnlyRepository> _repository;

    public AddressUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<IAddressUpdateOnlyRepository>();
    }
    public AddressUpdateOnlyRepositoryBuilder GetById(Address address)
    {
        _repository.Setup(a => a.GetById(address.Id)).ReturnsAsync(address);
        return this;
    }
    public IAddressUpdateOnlyRepository Build() => _repository.Object;
}

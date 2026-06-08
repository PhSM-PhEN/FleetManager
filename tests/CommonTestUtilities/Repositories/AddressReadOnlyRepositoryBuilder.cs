using System;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToAddress;
using Moq;

namespace CommonTestUtilities.Repositories;

public class AddressReadOnlyRepositoryBuilder
{
    private readonly Mock<IAddressReadOnlyRepository> _repository;
    public AddressReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IAddressReadOnlyRepository>();
    }
    public AddressReadOnlyRepositoryBuilder GetById(Address address)
    {
        _repository.Setup(a => a.GetById(address.Id)).ReturnsAsync(address);
        return this;
    }
    public AddressReadOnlyRepositoryBuilder GetAll(List<Address> address)
    {
        _repository.Setup(a => a.GetAll()).ReturnsAsync(address);
        return this;
    }

    public IAddressReadOnlyRepository Build() => _repository.Object;
}

using System;
using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToAddress;

public interface IAddressUpdateOnlyRepository
{
    void Update(Address address);
    Task<Address?> GetById(long id);
}

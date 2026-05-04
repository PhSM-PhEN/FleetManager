using System;
using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToAddress;

public interface IAddressReadOnlyRepository
{
    Task< List<Address>> GetAll();
    Task<Address> GetById(long id);
}

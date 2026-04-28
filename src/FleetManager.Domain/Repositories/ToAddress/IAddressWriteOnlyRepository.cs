using System;
using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToAddress;

public interface IAddressWriteOnlyRepository
{
    Task Add(Address address);
    
    Task Delete (long id);
}

using System;
using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToCompany;

public interface ICompanyWriteOnlyRepository
{
    Task Add(Company company);
    Task Delete(int id);
}

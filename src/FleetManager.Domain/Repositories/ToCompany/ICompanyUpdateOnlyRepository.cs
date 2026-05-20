using System;
using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToCompany;

public interface ICompanyUpdateOnlyRepository
{
    Task<Company?> GetById(int id);
    void Update(Company company);
}

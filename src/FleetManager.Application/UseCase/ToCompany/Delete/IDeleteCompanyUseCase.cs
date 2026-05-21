using System;

namespace FleetManager.Application.UseCase.ToCompany.Delete;

public interface IDeleteCompanyUseCase
{
    Task Execute(int id);
}

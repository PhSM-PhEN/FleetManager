using System;
using FleetManager.communication.Responses.ToCompany;

namespace FleetManager.Application.UseCase.ToCompany.GetById;

public interface IGetByIdCompanyUseCase
{
    Task<ResponseCompanyJson> Execute(int id);
}

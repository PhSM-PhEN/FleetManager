using System;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToCompany.GetById;

public interface IGetByIdCompanyUseCase
{
    Task<ResponseCompanyJson> Execute(int id);
}

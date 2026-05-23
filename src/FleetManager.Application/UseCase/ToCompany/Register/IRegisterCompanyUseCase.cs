using System;
using FleetManager.communication.Requests;
using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToCompany.Register;

public interface IRegisterCompanyUseCase 
{
    Task<ResponseCompanyJson> Execute(RequestCompanyJson request);
}

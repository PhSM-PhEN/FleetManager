using System;
using FleetManager.communication.Requests.ToCompany;
using FleetManager.communication.Responses.ToCompany;

namespace FleetManager.Application.UseCase.ToCompany.Register;

public interface IRegisterCompanyUseCase 
{
    Task<ResponseCompanyJson> Execute(RequestCompanyJson request);
}

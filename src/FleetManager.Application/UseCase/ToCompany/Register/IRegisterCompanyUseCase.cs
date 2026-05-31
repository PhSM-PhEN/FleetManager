using System;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToCompany.Register;

public interface IRegisterCompanyUseCase 
{
    Task<ResponseCompanyJson> Execute(RequestCompanyJson request);
}

using System;
using FleetManager.communication.Requests;
using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToAddress.Register;

public interface IRequestAdressUseCase
{
    Task<ResponseAddressJson> Execute(RequestAddressJson request);
}

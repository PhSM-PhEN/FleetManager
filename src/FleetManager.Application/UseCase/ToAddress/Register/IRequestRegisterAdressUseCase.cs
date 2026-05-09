using System;
using FleetManager.communication.Requests.ToAddress;
using FleetManager.communication.Resposnes.ToAddress;

namespace FleetManager.Application.UseCase.ToAddress.Register;

public interface IRequestAdressUseCase
{
    Task<ResponseAddressJson> Execute(RequestAddressJson request);
}

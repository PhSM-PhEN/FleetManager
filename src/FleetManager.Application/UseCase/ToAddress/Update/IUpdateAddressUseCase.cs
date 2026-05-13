using System;
using FleetManager.communication.Requests.ToAddress;
using FleetManager.communication.Resposnes.ToAddress;

namespace FleetManager.Application.UseCase.ToAddress.Update;

public interface IUpdateAddressUseCase
{
    Task<ResponseAddressJson> Execute(long id , RequestAddressJson request);
}

using System;
using FleetManager.communication.Requests.ToAddress;

namespace FleetManager.Application.UseCase.ToAddress.Update;

public interface IUpdateAddressUseCase
{
    Task Execute(long id , RequestAddressJson request);
}

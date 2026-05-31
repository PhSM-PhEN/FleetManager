using System;
using FleetManager.Communication.Requests;

namespace FleetManager.Application.UseCase.ToAddress.Update;

public interface IUpdateAddressUseCase
{
    Task Execute(long id , RequestAddressJson request);
}

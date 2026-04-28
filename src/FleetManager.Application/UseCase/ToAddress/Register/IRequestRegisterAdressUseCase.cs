using System;
using FleetManager.communication.Resposnes.ToAddress;

namespace FleetManager.Application.UseCase.ToAddress.Register;

public interface IRequestRegisterAdressUseCase
{
    Task<ResponseAddressJson> Execute(RequestRegisterAddressUseCase request);
}

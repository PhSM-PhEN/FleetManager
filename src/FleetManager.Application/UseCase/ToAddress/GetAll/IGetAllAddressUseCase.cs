using System;
using FleetManager.communication.Resposnes.ToAddress;

namespace FleetManager.Application.UseCase.ToAddress.GetAll;

public interface IGetAllAddressUseCase
{
    Task <ResponseListAddressJson> Execute ();
}

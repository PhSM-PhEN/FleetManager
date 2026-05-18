using System;
using FleetManager.communication.Responses.ToAddress;

namespace FleetManager.Application.UseCase.ToAddress.GetAll;

public interface IGetAllAddressUseCase
{
    Task <ResponseListAddressJson> Execute ();
}

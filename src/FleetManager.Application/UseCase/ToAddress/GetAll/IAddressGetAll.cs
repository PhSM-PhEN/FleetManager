using System;
using FleetManager.communication.Resposnes.ToAddress;

namespace FleetManager.Application.UseCase.ToAddress.GetAll;

public interface IAddressGetAll
{
    Task <ResponseAddressJson> Execute ();
}

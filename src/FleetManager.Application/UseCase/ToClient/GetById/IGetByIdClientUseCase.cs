using System;
using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToClient.GetById;

public interface IGetByIdClientUseCase
{
    Task<ResponseClientJson> Execute(long id);
}

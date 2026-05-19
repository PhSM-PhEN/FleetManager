using System;
using FleetManager.communication.Responses.ToClient;

namespace FleetManager.Application.UseCase.ToClient.GetById;

public interface IGetByIdClientUseCase
{
    Task<ResponseClientJson> Execute(long id);
}

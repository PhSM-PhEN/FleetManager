using System;
using FleetManager.communication.Responses.ToClient;

namespace FleetManager.Application.UseCase.ToClient.GetAll;

public interface IGetAllClientUseCase
{
    Task<ResponseListClientJson> Execute();
}

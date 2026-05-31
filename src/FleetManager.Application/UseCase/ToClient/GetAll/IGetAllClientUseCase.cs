using System;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToClient.GetAll;

public interface IGetAllClientUseCase
{
    Task<ResponseListClientJson> Execute();
}

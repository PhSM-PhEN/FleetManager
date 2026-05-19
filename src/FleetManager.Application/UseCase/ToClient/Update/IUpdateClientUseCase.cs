using System;
using FleetManager.communication.Requests.ToClient;

namespace FleetManager.Application.UseCase.ToClient.Update;

public interface IUpdateClientUseCase
{
    Task Execute(long id, RequestClientJson request);
}

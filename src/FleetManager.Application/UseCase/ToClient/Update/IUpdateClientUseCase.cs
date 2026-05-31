using System;
using FleetManager.Communication.Requests;

namespace FleetManager.Application.UseCase.ToClient.Update;

public interface IUpdateClientUseCase
{
    Task Execute(long id, RequestClientJson request);
}

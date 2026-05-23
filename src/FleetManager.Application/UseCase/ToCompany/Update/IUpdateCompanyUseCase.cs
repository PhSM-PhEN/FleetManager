using System;
using FleetManager.communication.Requests;

namespace FleetManager.Application.UseCase.ToCompany.Update;

public interface IUpdateCompanyUseCase
{
    Task Execute(int id, RequestCompanyJson request);
}

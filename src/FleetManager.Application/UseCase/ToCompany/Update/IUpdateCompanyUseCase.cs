using System;
using FleetManager.Communication.Requests;

namespace FleetManager.Application.UseCase.ToCompany.Update;

public interface IUpdateCompanyUseCase
{
    Task Execute(int id, RequestUpdateCompanyJson request);
}

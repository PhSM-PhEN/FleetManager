using System;
using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToRentalPlan.GetById;

public interface IGetByIdRentalPlanUseCase
{
    Task<ResponseRentalPlanJson> Execute(int id);
}

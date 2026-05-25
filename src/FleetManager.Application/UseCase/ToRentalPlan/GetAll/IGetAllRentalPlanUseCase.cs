using System;
using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToRentalPlan.GetAll;

public interface IGetAllRentalPlanUseCase
{
    Task<ResponseListRentalPlanJson> Execute();
}

using System;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToRentalPlan.GetAll;

public interface IGetAllRentalPlanUseCase
{
    Task<ResponseListRentalPlanJson> Execute();
}

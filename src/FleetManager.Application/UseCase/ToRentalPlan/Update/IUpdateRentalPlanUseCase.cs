using System;
using FleetManager.Communication.Requests;

namespace FleetManager.Application.UseCase.ToRentalPlan.Update;

public interface IUpdateRentalPlanUseCase
{
    Task Execute(int id, RequestRentalPlansJson request);
}

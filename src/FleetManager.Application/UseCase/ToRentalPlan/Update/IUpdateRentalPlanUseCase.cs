using System;
using FleetManager.communication.Requests;

namespace FleetManager.Application.UseCase.ToRentalPlan.Update;

public interface IUpdateRentalPlanUseCase
{
    Task Execute(int id, RequestRentalPlansJson request);
}

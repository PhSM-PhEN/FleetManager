using System;

namespace FleetManager.Application.UseCase.ToRentalPlan.Disable;

public interface IDisableRentalPlanUseCase
{
    Task Execute(int id);
}

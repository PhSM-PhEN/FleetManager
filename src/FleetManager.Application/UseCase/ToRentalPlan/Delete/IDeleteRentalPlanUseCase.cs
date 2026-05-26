using System;

namespace FleetManager.Application.UseCase.ToRentalPlan.Delete;

public interface IDeleteRentalPlanUseCase
{
    Task Execute(int id);
}

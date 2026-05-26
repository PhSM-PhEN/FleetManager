using System;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToRentalPlans;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRentalPlan.Delete;

public class DeleteRentalPlanUseCase(IUnitOfWork unitOfWork, IRentalPlansWriteOnlyRepository repository, IRentalPlansUpdateOnlyRepository updateOnlyRepository) : IDeleteRentalPlanUseCase
{
    public async Task Execute(int id)
    {
        var rentalPlan = await updateOnlyRepository.GetById(id);
        if(rentalPlan is null)
        {
            throw new NotFoundException("Rental plan not found");
        }
        await repository.Delete(rentalPlan.Id);
        await unitOfWork.Commit();
    }
}

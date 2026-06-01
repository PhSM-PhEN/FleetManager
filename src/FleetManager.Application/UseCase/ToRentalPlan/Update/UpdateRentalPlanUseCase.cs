using AutoMapper;
using FleetManager.Communication.Requests;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToRentalPlans;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRentalPlan.Update;

public class UpdateRentalPlanUseCase(IMapper mapper, IRentalPlansUpdateOnlyRepository repository, IUnitOfWork unitOfWork) : IUpdateRentalPlanUseCase
{
    public async Task Execute(int id, RequestRentalPlansJson request)
    {
        Validate(request);
        var rentalPlan = await repository.GetById(id);
        if(rentalPlan is null)
        {
            throw new NotFoundException("Rental plan not found");
        }
        mapper.Map(request, rentalPlan);
        repository.Update(rentalPlan);
        await unitOfWork.Commit();


    }
    public static void Validate(RequestRentalPlansJson request)
    {
        var validator = new RentalPlanValidator();
        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessage);
        }
    }
}


using AutoMapper;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToRentalPlans;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRentalPlan.GetById;

public class GetByIdRentalPlanUseCase(IMapper mapper, IRentalPlansReadOnlyRepository repository) : IGetByIdRentalPlanUseCase
{
    public async Task<ResponseRentalPlanJson> Execute(long id)
    {
        var rentalPlan = await repository.GetById(id);
        if(rentalPlan is null)
        {
            throw new NotFoundException("Rental plan not found");
        }

        return mapper.Map<ResponseRentalPlanJson>(rentalPlan) ;
    }
}

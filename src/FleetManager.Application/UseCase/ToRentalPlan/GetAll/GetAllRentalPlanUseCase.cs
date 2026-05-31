using System;
using AutoMapper;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToRentalPlans;
using FleetManager.Exception.ExceptionBase;
namespace FleetManager.Application.UseCase.ToRentalPlan.GetAll;

public class GetAllRentalPlanUseCase(IMapper mapper, IRentalPlansReadOnlyRepository repository) : IGetAllRentalPlanUseCase
{
    public async Task<ResponseListRentalPlanJson> Execute()
    {
        var rentalPlan = await repository.GetAll();
        if(rentalPlan.Count == 0)
        {
            throw new NotFoundException("Rental plan not found.");
        }
        return new ResponseListRentalPlanJson
        {
            RentalPlans = mapper.Map<List<ResponseShortRentalPlansJson>>(rentalPlan)
        };
         
    }
}

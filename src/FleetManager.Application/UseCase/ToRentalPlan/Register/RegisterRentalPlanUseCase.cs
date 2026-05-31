using AutoMapper;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToRentalPlans;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRentalPlan.Register
{
    public class RegisterRentalPlanUseCase(IRentalPlansWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : IRegisterRentalPlanUseCase
    {
        public async Task<ResponseRentalPlanJson> Execute(RequestRentalPlansJson request)
        {
            Validate(request);

            var rentalPlan = mapper.Map<RentalPlan>(request);

            await repository.Add(rentalPlan);
            await unitOfWork.Commit();

            return mapper.Map<ResponseRentalPlanJson>(rentalPlan);
        }
        private static void Validate(RequestRentalPlansJson request)
        {
            var validator = new RentalPlanValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList(); 
                throw new ErrorOnValidationException(errorMessage);
            }


        }
    }
}

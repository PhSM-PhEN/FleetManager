using FleetManager.Application.Extensions;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToRentalPlans;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRentalPlan.Register
{
    public class RegisterRentalPlanUseCase(IRentalPlansWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IRegisterRentalPlanUseCase
    {
        public async Task<ResponseRentalPlanJson> Execute(RequestRentalPlansJson request)
        {
            Validate(request);

            var rentalPlan = new RentalPlan(request.Name, (Domain.Enums.RentalMode)request.Mode,
             (Domain.Enums.TransmissionType)request.Transmission, request.PriceRental, request.TotalKmIncluded, request.PricePerKm);

            await repository.Add(rentalPlan);
            await unitOfWork.Commit();

            return rentalPlan.ToDetailResponse();
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
